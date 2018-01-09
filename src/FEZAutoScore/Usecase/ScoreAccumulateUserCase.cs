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
        public ScoreDataGridColumnVisibleSetting ColumnVisibleSetting { get; }
        public AppSetting AppSetting { get; }

        private bool _isAccumulating = false;
        private CancellationTokenSource _tokenSource = null;
        private Task _accumulateTask = null;
        private ScoreRepository _scoreRepository = null;

        public ScoreAccumulateUseCase()
        {
            try
            {
                _scoreRepository = new ScoreRepository();

                var settingRepository = new SettingRepository();
                ColumnVisibleSetting = settingRepository.GetColumnVisibleSetting();
                AppSetting = settingRepository.GetAppSetting();

                foreach (var score in _scoreRepository.ScoreDbSet.OrderBy(x => x.記録日時))
                {
                    RegisterToDbUpdateWhenPropertyChanged(score);
                    ScoreCollection.Add(score);
                }

                LatestScore.Value = ScoreCollection.LastOrDefault() ?? new ScoreEntity();
            }
            catch
            {
                // TODO: error handling
                throw;
            }
        }

        public void Dispose()
        {
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
                var shooter = new FEZScreenShooter();
                var analyzer = new FEZScoreAnalyzer();
                var screenShotRepository = new ScoreScreenShotRepository();
                var scoreFileRepository = new ScoreFileRepository();

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        UpdateState(ScoreAccumulatingState.Monitoring);

                        var ret = await AccumulateScoreAsync(
                            shooter, analyzer, scoreFileRepository, screenShotRepository, _scoreRepository, token);

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
            catch (Exception)
            {
                // TODO: error handling
                throw;
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
            var text = AverageScoreTextFormatter.ToString(AppSetting.AverageScoreTextFormat.Value, scores.Where(x => x.集計対象));

            Clipboard.SetText(text);
        }

        public void CopyEachScoreToClipboard(IEnumerable<ScoreEntity> scores)
        {
            var text = ScoreTextFormatter.ToString(AppSetting.ScoreTextFormat.Value, scores.Where(x => x.集計対象));

            Clipboard.SetText(text);
        }

        public async Task SaveAsCsvAsync()
        {
            var scoreFileRepository = new ScoreFileRepository();
            await scoreFileRepository.SaveAsCsvAsync(ScoreCollection);
        }

        public void UpdateAppSetting(AppSetting appSetting)
        {
            AppSetting.AverageScoreTextFormat.Value = appSetting.AverageScoreTextFormat.Value;
            AppSetting.IsAutoImageSave.Value = appSetting.IsAutoImageSave.Value;
            AppSetting.IsLatestScoreOutputAsText.Value = appSetting.IsLatestScoreOutputAsText.Value;
            AppSetting.LatestScoreTextFormat.Value = appSetting.LatestScoreTextFormat.Value;
            AppSetting.ScoreTextFormat.Value = appSetting.ScoreTextFormat.Value;
        }

        private enum AccumulateResult
        {
            FEZNotRunning,
            NotScoreCapture,
            Successed,
        }

        private async Task<AccumulateResult> AccumulateScoreAsync(
            FEZScreenShooter shooter, FEZScoreAnalyzer analyzer, ScoreFileRepository scoreFileRepository,
            ScoreScreenShotRepository screenShotRepository, ScoreRepository scoreRepository,
            CancellationToken token)
        {
            try
            {
                // スクリーンショットを取得
                using (var bitmap = shooter.Shoot())
                //using (var bitmap = new System.Drawing.Bitmap(@"D:\src\FEZAutoScore\src\FEZAutoScore\bin\Debug - コピー\screenshot\20180105_074218.png"))
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
                    await screenShotRepository.SaveAsPngAsync(score, bitmap);

                    // テキスト保存
                    await scoreFileRepository.SaveAsLatestScoreAsync(score);

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
            catch (Exception)
            {
                // TODO: error handling
                throw;
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
                try
                {
                    _scoreRepository.ScoreDbSet.Update(score);
                    await _scoreRepository.SaveChangesAsync();
                }
                catch
                {
                    // TODO: error handling
                    throw;
                }
            };
        }
    }
}
