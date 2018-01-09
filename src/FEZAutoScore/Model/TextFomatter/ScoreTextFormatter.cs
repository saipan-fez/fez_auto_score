using FEZAutoScore.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FEZAutoScore.Model.TextFomatter
{
    public class BaseScoreTextFormatter
    {
        public static string ToKillo(double value)
        {
            return Math.Round((value / 1000.0), 1, MidpointRounding.AwayFromZero) + "k";
        }
    }

    public class ScoreTextFormatter : BaseScoreTextFormatter
    {
        public const string WarResultFormat = "{勝敗}";
        public const string WorkFormat = "{職業}";
        public const string WarTimeFormat = "{戦争時間}";
        public const string MapFormat = "{マップ名}";
        public const string KillCountFormat = "{キル数}";
        public const string DeadCountFormat = "{デッド数}";
        public const string PCDFormat = "{PC与ダメージ}";
        public const string PCDKilloFormat = "{PC与ダメージ_K}";
        public const string BDFormat = "{建築与ダメージ}";
        public const string BDKilloFormat = "{建築与ダメージ_K}";

        public static string ToString(string format, IEnumerable<ScoreEntity> scores)
        {
            return string.Join(Environment.NewLine, scores.Select(x => ToString(format, x)));
        }

        public static string ToString(string format, ScoreEntity score)
        {
            return format
                .Replace(WarResultFormat, score.結果.ToString())
                .Replace(WorkFormat, score.職業.ToString())
                .Replace(WarTimeFormat, score.戦争継続時間.ToString("mm\\:ss"))
                .Replace(MapFormat, score.Map名)
                .Replace(KillCountFormat, score.キル数.ToString())
                .Replace(DeadCountFormat, score.デッド数.ToString())
                .Replace(PCDFormat, score.PC与ダメージ.ToString())
                .Replace(PCDKilloFormat, ToKillo(score.PC与ダメージ))
                .Replace(PCDFormat, score.建築与ダメージ.ToString())
                .Replace(PCDKilloFormat, ToKillo(score.建築与ダメージ));
        }
    }

    public class AverageScoreTextFormatter : BaseScoreTextFormatter
    {
        public const string WarCountFormat = "{戦争数}";
        public const string WinRateFormat = "{勝率}";
        public const string AverageWarTimeFormat = "{平均戦争時間}";
        public const string AverageKillCountFormat = "{平均キル数}";
        public const string AverageDeadCountFormat = "{平均デッド数}";
        public const string AveragePCDFormat = "{平均PC与ダメージ}";
        public const string AveragePCDKilloFormat = "{平均PC与ダメージ_K}";
        public const string AverageBDFormat = "{平均建築与ダメージ}";
        public const string AverageBDKilloFormat = "{平均建築与ダメージ_K}";

        public static string ToString(string format, IEnumerable<ScoreEntity> scores)
        {
            var warCount = scores.Count();
            var winRate = Math.Round((double)scores.Count(x => x.結果 == WarResult.Win) / scores.Count() * 100, 1, MidpointRounding.AwayFromZero) + "%";
            var warTime = new TimeSpan((long)scores.Average(x => x.戦争継続時間.Ticks)).ToString("mm\\:ss");
            var kill = Math.Round(scores.Average(x => x.キル数), 1, MidpointRounding.AwayFromZero);
            var dead = Math.Round(scores.Average(x => x.デッド数), 1, MidpointRounding.AwayFromZero);
            var pcd = Math.Round(scores.Average(x => x.PC与ダメージ), 1, MidpointRounding.AwayFromZero);
            var pcdKillo = ToKillo(Math.Round(scores.Average(x => x.PC与ダメージ), 1, MidpointRounding.AwayFromZero));
            var bd = Math.Round(scores.Average(x => x.建築与ダメージ), 1, MidpointRounding.AwayFromZero);
            var bdKillo = ToKillo(Math.Round(scores.Average(x => x.建築与ダメージ), 1, MidpointRounding.AwayFromZero));

            return format
                .Replace(WarCountFormat, warCount.ToString())
                .Replace(WinRateFormat, winRate)
                .Replace(AverageWarTimeFormat, warTime)
                .Replace(AverageKillCountFormat, kill.ToString())
                .Replace(AverageDeadCountFormat, dead.ToString())
                .Replace(AveragePCDFormat, pcd.ToString())
                .Replace(AveragePCDKilloFormat, pcdKillo)
                .Replace(AverageBDFormat, bd.ToString())
                .Replace(AverageBDKilloFormat, bdKillo);
        }
    }

    public class CsvScoreTextFormatter : BaseScoreTextFormatter
    {
        private const string Delimiter = ",";

        public static string ToString(IEnumerable<ScoreEntity> scores)
        {
            var header = string.Join(Delimiter,
                nameof(ScoreEntity.集計対象), nameof(ScoreEntity.記録日時), nameof(ScoreEntity.Map名), nameof(ScoreEntity.結果),
                nameof(ScoreEntity.戦争継続時間), nameof(ScoreEntity.戦闘), nameof(ScoreEntity.領域), nameof(ScoreEntity.支援),
                nameof(ScoreEntity.PC与ダメージ), nameof(ScoreEntity.キルダメージボーナス), nameof(ScoreEntity.召喚解除ボーナス),
                nameof(ScoreEntity.建築与ダメージ),  nameof(ScoreEntity.領域破壊ボーナス), nameof(ScoreEntity.領域ダメージボーナス),
                nameof(ScoreEntity.貢献度), nameof(ScoreEntity.クリスタル運用ボーナス), nameof(ScoreEntity.召喚行動ボーナス),
                nameof(ScoreEntity.キル数),  nameof(ScoreEntity.デッド数), nameof(ScoreEntity.建築数), nameof(ScoreEntity.建築物破壊数),
                nameof(ScoreEntity.クリスタル採掘量), nameof(ScoreEntity.職業),
                nameof(ScoreEntity.スキル1), nameof(ScoreEntity.スキル2), nameof(ScoreEntity.スキル3),
                nameof(ScoreEntity.スキル4), nameof(ScoreEntity.スキル5), nameof(ScoreEntity.スキル6),
                nameof(ScoreEntity.スキル7), nameof(ScoreEntity.スキル8), nameof(ScoreEntity.備考));

            var values = scores.Select(x => string.Join(Delimiter,
                x.集計対象,  x.記録日時, x.Map名, x.結果,
                x.戦争継続時間, x.戦闘, x.領域, x.支援,
                x.PC与ダメージ, x.キルダメージボーナス, x.召喚解除ボーナス,
                x.建築与ダメージ, x.領域破壊ボーナス, x.領域ダメージボーナス,
                x.貢献度, x.クリスタル運用ボーナス, x.召喚行動ボーナス,
                x.キル数, x.デッド数, x.建築数, x.建築物破壊数,
                x.クリスタル採掘量, x.職業,
                x.スキル1, x.スキル2, x.スキル3,
                x.スキル4, x.スキル5, x.スキル6,
                x.スキル7, x.スキル8, x.備考));

            return
                header + Environment.NewLine +
                string.Join(Environment.NewLine, values);
        }
    }
}
