using Reactive.Bindings;
using static FEZAutoScore.Model.TextFomatter.AverageScoreTextFormatter;
using static FEZAutoScore.Model.TextFomatter.ScoreTextFormatter;

namespace FEZAutoScore.Model.Setting
{
    public class AppSetting : BaseSetting
    {
        private static readonly string DefaultAverageScoreTextFormat = 
            $"【戦争数】{WarCountFormat}戦\r\n" +
            $"【勝率】{WinRateFormat}\r\n" +
            $"【キル】{AverageKillCountFormat}\r\n" +
            $"【デッド】{AverageDeadCountFormat}\r\n" +
            $"【PCD】{AveragePCDKilloFormat}";

        private static readonly string DefaultScoreTextFormat =
            $"{WarResultFormat} {MapFormat} {KillCountFormat}kill {DeadCountFormat}dead {PCDKilloFormat}";

        private static readonly string DefaultLatestScoreTextFormat =
            $"{KillCountFormat}kill {DeadCountFormat}dead {PCDKilloFormat}";

        public ReactiveProperty<bool> IsAutoImageSave { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> IsLatestScoreOutputAsText { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<string> AverageScoreTextFormat { get; } = new ReactiveProperty<string>(DefaultAverageScoreTextFormat);
        public ReactiveProperty<string> ScoreTextFormat { get; } = new ReactiveProperty<string>(DefaultScoreTextFormat);
        public ReactiveProperty<string> LatestScoreTextFormat { get; } = new ReactiveProperty<string>(DefaultLatestScoreTextFormat);
        public ReactiveProperty<bool> IsAccumulatingAtLastTime { get; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> IsFirstBoot { get; } = new ReactiveProperty<bool>(true);

        public AppSetting Clone()
        {
            var setting = new AppSetting();
            setting.IsAutoImageSave.Value = IsAutoImageSave.Value;
            setting.IsLatestScoreOutputAsText.Value = IsLatestScoreOutputAsText.Value;
            setting.AverageScoreTextFormat.Value = AverageScoreTextFormat.Value;
            setting.ScoreTextFormat.Value = ScoreTextFormat.Value;
            setting.LatestScoreTextFormat.Value = LatestScoreTextFormat.Value;
            setting.IsAccumulatingAtLastTime.Value = IsAccumulatingAtLastTime.Value;
            setting.IsFirstBoot.Value = IsFirstBoot.Value;

            return setting;
        }
    }
}
