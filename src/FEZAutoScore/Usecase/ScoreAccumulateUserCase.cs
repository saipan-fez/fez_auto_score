using FEZAutoScore.Model.Entity;
using FEZAutoScore.Model.Analyzer;
using FEZAutoScore.Model.ScreenShot;
using FEZAutoScore.Model.Repository;
using Reactive.Bindings;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using FEZAutoScore.Model.Setting;
using System.Windows;
using System.Collections.Generic;
using FEZAutoScore.Model.TextFomatter;
using Microsoft.EntityFrameworkCore;

namespace FEZAutoScore.Usecase
{
    public enum ScoreAccumulatingState
    {
        Stopped,
        Monitoring,
        Sleeping,
        Stopping,
    }

    public class ScoreAccumulateUseCase : IDisposable
    {
        private const int DetectSleepMilliSecond = 5 * 60 * 1000;
        private const int SleepMilliSecond = 2 * 1000;
        private const int DeepSleepMilliSecond = 1 * 60 * 1000;

        public ReactiveCollection<ScoreEntity> ScoreCollection { get; } = new ReactiveCollection<ScoreEntity>();
        public ReactiveProperty<ScoreAccumulatingState> State { get; } = new ReactiveProperty<ScoreAccumulatingState>(ScoreAccumulatingState.Stopped);
        public ReactiveProperty<ScoreEntity> LatestScore { get; } = new ReactiveProperty<ScoreEntity>();
        public ReactiveProperty<ScoreDataGridColumnVisibleSetting> ColumnVisibleSetting { get; } = new ReactiveProperty<ScoreDataGridColumnVisibleSetting>();
        public ReactiveProperty<AppSetting> AppSetting { get; } = new ReactiveProperty<AppSetting>();

        private bool _isAccumulating = false;
        private CancellationTokenSource _tokenSource = null;
        private Task _accumulateTask = null;

        private ScoreRepository _scoreRepository = null;
        private ScoreScreenShotRepository _scoreScreenShotRepository = null;
        private ScoreFileRepository _scoreFileRepository = null;
        private FEZScreenShooter _fezScreenShooter = null;
        private FEZScoreAnalyzer _fezScoreAnalyzer = null;

        public ScoreAccumulateUseCase()
        {
        }

        public async Task InitializeAsync()
        {
            await Task.Run(() =>
            {
                try
                {
                    var settingRepository = new SettingRepository();
                    ColumnVisibleSetting.Value = settingRepository.GetColumnVisibleSetting();
                    AppSetting.Value = settingRepository.GetAppSetting();

                    var isAutoImageSave = AppSetting.Value.IsAutoImageSave.Value;
                    var isLatestScoreOutputAsText = AppSetting.Value.IsLatestScoreOutputAsText.Value;

                    _scoreRepository = new ScoreRepository();
                    _scoreScreenShotRepository = ScoreScreenShotRepository.Create(isAutoImageSave);
                    _scoreFileRepository = ScoreFileRepository.Create(isLatestScoreOutputAsText);
                    _fezScreenShooter = new FEZScreenShooter();
                    _fezScoreAnalyzer = new FEZScoreAnalyzer();

                    // DBファイルが無ければ作成、作成済みだが古いバージョンの場合は自動で最新のテーブル構成に更新する
                    _scoreRepository.Database.Migrate();

                    foreach (var score in _scoreRepository.ScoreDbSet.OrderBy(x => x.記録日時))
                    {
                        RegisterToDbUpdateWhenPropertyChanged(score);
                        ScoreCollection.Add(score);
                    }

                    LatestScore.Value = ScoreCollection.LastOrDefault() ?? new ScoreEntity();

                    // 監視状態ONの状態で終了していた場合は、初期化時に監視を開始する
                    if (AppSetting.Value.IsAccumulatingAtLastTime.Value)
                    {
                        StartToAccumulateScore();
                    }
                }
                catch (Exception ex)
                {
                    ApplicationError.HandleUnexpectedError(ex);
                }
            });

            // 初回起動時は案内メッセージを表示する(2回目以降は表示しない)
            if (AppSetting.Value.IsFirstBoot.Value)
            {
                MessageBox.Show(Properties.Resources.FirstBootInfoMessage, "Info");

                AppSetting.Value.IsFirstBoot.Value = false;
            }
        }

        public void Dispose()
        {
            // 次回起動時に監視状態を維持するため監視状態を保存
            AppSetting.Value.IsAccumulatingAtLastTime.Value = _isAccumulating;

            StopToAccumulateScore();

            if (_scoreRepository != null)
            {
                _scoreRepository.Dispose();
                _scoreRepository = null;
            }
        }

        public void StartToAccumulateScore()
        {
            if (_isAccumulating)
            {
                return;
            }

            _isAccumulating = true;

            _tokenSource = new CancellationTokenSource();

            _accumulateTask = Task.Run(async () =>
            {
                var token = _tokenSource.Token;

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        UpdateState(ScoreAccumulatingState.Monitoring);

                        var ret = await AccumulateScoreAsync(
                            _fezScreenShooter, _fezScoreAnalyzer, _scoreFileRepository,
                            _scoreScreenShotRepository, _scoreRepository, AppSetting.Value,
                            token);

                        switch (ret)
                        {
                            case AccumulateResult.FEZNotRunning:
                                await Task.Delay(DeepSleepMilliSecond, token);
                                break;

                            case AccumulateResult.NotScoreCapture:
                                await Task.Delay(SleepMilliSecond, token);
                                break;

                            case AccumulateResult.Successed:
                                // 同じ戦争のスコアを複数回検出しないよう、5分間Waitする
                                UpdateState(ScoreAccumulatingState.Sleeping);
                                await Task.Delay(DetectSleepMilliSecond, token);
                                break;

                            default:
                                break;
                        }
                    }
                }
                catch (TaskCanceledException)
                {
                    // nop
                }
            });
        }

        public void StopToAccumulateScore()
        {
            if (!_isAccumulating)
            {
                return;
            }

            try
            {
                UpdateState(ScoreAccumulatingState.Stopping);

                _tokenSource.Cancel(false);

                _accumulateTask.Wait();
            }
            catch (Exception ex)
            {
                ApplicationError.HandleUnexpectedError(ex);
            }
            finally
            {
                UpdateState(ScoreAccumulatingState.Stopped);

                _tokenSource.Dispose();
                _tokenSource = null;

                _isAccumulating = false;
            }
        }

        public void CopyAverageScoreToClipboard(IEnumerable<ScoreEntity> scores)
        {
            var text = AverageScoreTextFormatter.ToString(AppSetting.Value.AverageScoreTextFormat.Value, scores.Where(x => x.集計対象));

            Clipboard.SetText(text);
        }

        public void CopyEachScoreToClipboard(IEnumerable<ScoreEntity> scores)
        {
            var text = ScoreTextFormatter.ToString(AppSetting.Value.ScoreTextFormat.Value, scores.Where(x => x.集計対象));

            Clipboard.SetText(text);
        }

        public async Task SaveAsCsvAsync()
        {
            await _scoreFileRepository.SaveAsCsvAsync(ScoreCollection);
        }

        public void UpdateAppSetting(AppSetting appSetting)
        {
            AppSetting.Value.AverageScoreTextFormat.Value = appSetting.AverageScoreTextFormat.Value;
            AppSetting.Value.IsAutoImageSave.Value = appSetting.IsAutoImageSave.Value;
            AppSetting.Value.IsLatestScoreOutputAsText.Value = appSetting.IsLatestScoreOutputAsText.Value;
            AppSetting.Value.LatestScoreTextFormat.Value = appSetting.LatestScoreTextFormat.Value;
            AppSetting.Value.ScoreTextFormat.Value = appSetting.ScoreTextFormat.Value;
            AppSetting.Value.IsAccumulatingAtLastTime.Value = appSetting.IsAccumulatingAtLastTime.Value;
            AppSetting.Value.IsFirstBoot.Value = appSetting.IsFirstBoot.Value;

            if (AppSetting.Value.IsAutoImageSave.Value)
            {
                _scoreScreenShotRepository.CreateDirectoryIfNotExists();
            }

            if (AppSetting.Value.IsLatestScoreOutputAsText.Value)
            {
                _scoreFileRepository.CreateDummyLatestScoreIfNotExists();
            }
        }

        public void OpenScreenShotFolder()
        {
            _scoreScreenShotRepository.OpenDirectory();
        }

        public void OpenLatestScoreFolder()
        {
            _scoreFileRepository.OpenDirectory();
        }

        private enum AccumulateResult
        {
            FEZNotRunning,
            NotScoreCapture,
            Successed,
        }

        private async Task<AccumulateResult> AccumulateScoreAsync(
            FEZScreenShooter shooter, FEZScoreAnalyzer analyzer, ScoreFileRepository scoreFileRepository,
            ScoreScreenShotRepository screenShotRepository, ScoreRepository scoreRepository, AppSetting appSetting,
            CancellationToken token)
        {
            try
            {
                // スクリーンショットを取得
                using (var bitmap = shooter.Shoot())
                {
                    if (bitmap == null)
                    {
                        return AccumulateResult.FEZNotRunning;
                    }

                    // スクリーンショットからスコアを取得
                    var score = analyzer.Analyze(bitmap);

                    if (score == null)
                    {
                        return AccumulateResult.NotScoreCapture;
                    }

                    RegisterToDbUpdateWhenPropertyChanged(score);

                    // 画像保存
                    if (appSetting.IsAutoImageSave.Value)
                    {
                        await screenShotRepository.SaveAsPngAsync(score, bitmap);
                    }

                    // テキスト保存
                    if (appSetting.IsLatestScoreOutputAsText.Value)
                    {
                        await scoreFileRepository.SaveAsLatestScoreAsync(appSetting.LatestScoreTextFormat.Value, score);
                    }

                    // DB保存
                    await scoreRepository.ScoreDbSet.AddAsync(score);
                    await scoreRepository.SaveChangesAsync();

                    // プロパティも更新
                    ScoreCollection.Add(score);
                    LatestScore.Value = score;
                }
            }
            catch (OperationCanceledException)
            { }
            catch (Exception ex)
            {
                ApplicationError.HandleUnexpectedError(ex);
            }

            return AccumulateResult.Successed;
        }

        private void UpdateState(ScoreAccumulatingState state)
        {
            State.Value = state;
        }

        private void RegisterToDbUpdateWhenPropertyChanged(ScoreEntity score)
        {
            score.PropertyChanged += async (s, e) =>
            {
                _scoreRepository.ScoreDbSet.Update(score);
                await _scoreRepository.SaveChangesAsync();
            };
        }
    }
}
