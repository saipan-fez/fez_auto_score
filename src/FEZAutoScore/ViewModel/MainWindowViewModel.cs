using FEZAutoScore.Model.Entity;
using FEZAutoScore.Model.Setting;
using FEZAutoScore.Usecase;
using FEZAutoScore.View;
using MaterialDesignThemes.Wpf;
using Reactive.Bindings;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace FEZAutoScore.ViewModel
{
    public class MainWindowViewModel
    {
        public ReactiveProperty<bool> IsLoading { get; }
        public ReadOnlyReactiveProperty<ScoreAccumulatingState> CurrentState { get; }
        public ReadOnlyReactiveCollection<ScoreEntity> ScoreCollection { get; }
        public ReactiveProperty<ScoreEntity> LatestScore { get; }
        public ReactiveProperty<ScoreDataGridColumnVisibleSetting> ColumnVisibleSetting { get; }

        public ReactiveCommand LoadedCommand { get; }
        public ReactiveCommand ClosedCommand { get; }
        public ReactiveCommand AccumulateStartCommand { get; }
        public ReactiveCommand AccumulateStopCommand { get; }
        public ReactiveCommand<object> CopyAverageScoreCommand { get; }
        public ReactiveCommand<object> CopyEachScoreCommand { get; }
        public ReactiveCommand SaveAsCsvCommand { get; }
        public ReactiveCommand ShowSettingDialogCommand { get; }

        public SnackbarMessageQueue MessageQueue { get; } = new SnackbarMessageQueue();

        private ScoreAccumulateUseCase _scoreAccumulateUseCase;

        public MainWindowViewModel()
        {
            _scoreAccumulateUseCase = new ScoreAccumulateUseCase();

            IsLoading = new ReactiveProperty<bool>(true);

            CurrentState = _scoreAccumulateUseCase.State
                .ToReadOnlyReactiveProperty();

            ScoreCollection = _scoreAccumulateUseCase.ScoreCollection
                .ToReadOnlyReactiveCollection();

            LatestScore = _scoreAccumulateUseCase.LatestScore
                .ToReactiveProperty();

            ColumnVisibleSetting = _scoreAccumulateUseCase.ColumnVisibleSetting
                .ToReactiveProperty();

            LoadedCommand = new ReactiveCommand();
            LoadedCommand.Subscribe(async () =>
            {
                await _scoreAccumulateUseCase.InitializeAsync();

                IsLoading.Value = false;
            });

            ClosedCommand = new ReactiveCommand();
            ClosedCommand.Subscribe(() =>
            {
                _scoreAccumulateUseCase.Dispose();
                _scoreAccumulateUseCase = null;
            });

            AccumulateStartCommand = _scoreAccumulateUseCase.State
                .Select(x => x == ScoreAccumulatingState.Stopped)
                .ToReactiveCommand();
            AccumulateStartCommand.Subscribe(() =>
            {
                _scoreAccumulateUseCase.StartToAccumulateScore();
            });

            AccumulateStopCommand = _scoreAccumulateUseCase.State
                .Select(x => x != ScoreAccumulatingState.Stopped)
                .ToReactiveCommand();
            AccumulateStopCommand.Subscribe(() =>
            {
                _scoreAccumulateUseCase.StopToAccumulateScore();
            });

            CopyAverageScoreCommand = new ReactiveCommand<object>();
            CopyAverageScoreCommand.Subscribe(Observer.Create<object>(o =>
            {
                var scores = ((System.Collections.IList)o).Cast<ScoreEntity>();
                _scoreAccumulateUseCase.CopyAverageScoreToClipboard(scores);

                MessageQueue.Enqueue("クリップボードにコピーしました");
            }));

            CopyEachScoreCommand = new ReactiveCommand<object>();
            CopyEachScoreCommand.Subscribe(Observer.Create<object>(o =>
            {
                var scores = ((System.Collections.IList)o).Cast<ScoreEntity>();
                _scoreAccumulateUseCase.CopyEachScoreToClipboard(scores);

                MessageQueue.Enqueue("クリップボードにコピーしました");
            }));

            SaveAsCsvCommand = new ReactiveCommand();
            SaveAsCsvCommand.Subscribe(async () =>
            {
                await _scoreAccumulateUseCase.SaveAsCsvAsync();
            });

            ShowSettingDialogCommand = new ReactiveCommand();
            ShowSettingDialogCommand.Subscribe(async () =>
            {
                // 設定をコピーしてダイアログを開く
                var appSetting = _scoreAccumulateUseCase.AppSetting.Value.Clone();
                var viewModel = new SettingDialogViewModel(appSetting);

                var view = new SettingDialog()
                {
                    DataContext = viewModel
                };

                var result = await DialogHost.Show(view, "RootDialog");

                // OKの時のみ設定を反映
                if ((bool)result)
                {
                    _scoreAccumulateUseCase.UpdateAppSetting(viewModel.AppSetting.Value);
                }
            });
        }
    }
}
