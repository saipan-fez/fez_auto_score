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
        public const string WarSideFormat = "{攻守}";
        public const string OffenseCountryFormat = "{攻撃側国名}";
        public const string DeffenseFormat = "{防衛側国名}";
        public const string MapFormat = "{マップ名}";
        public const string WorkFormat = "{職業}";
        public const string WarTimeFormat = "{戦争時間}";
        public const string BattleScoreFormat = "{戦闘}";
        public const string RegionScoreFormat = "{領域}";
        public const string SupportScoreFormat = "{支援}";
        public const string PCDFormat = "{PC与ダメージ}";
        public const string PCDKilloFormat = "{PC与ダメージ_K}";
        public const string KillDamageBonusFormat = "{キルダメージボーナス}";
        public const string SummonReleaseBonusFormat = "{召喚解除ボーナス}";
        public const string BDFormat = "{建築与ダメージ}";
        public const string BDKilloFormat = "{建築与ダメージ_K}";
        public const string RegionDestroyBonusFormat = "{領域破壊ボーナス}";
        public const string RegionDamageBonusFormat = "{領域ダメージボーナス}";
        public const string ContributionScoreFormat = "{貢献度}";
        public const string CrystalInvestmentBonusFormat = "{クリスタル運用ボーナス}";
        public const string SummonBonusFormat = "{召喚行動ボーナス}";
        public const string KillCountFormat = "{キル数}";
        public const string DeadCountFormat = "{デッド数}";
        public const string BuildCountFormat = "{建築数}";
        public const string BuildDestroyCountFormat = "{建築物破壊数}";
        public const string CrystalMiningFormat = "{クリスタル採掘量}";
        public const string Skill1Format = "{スキル1}";
        public const string Skill2Format = "{スキル2}";
        public const string Skill3Format = "{スキル3}";
        public const string Skill4Format = "{スキル4}";
        public const string Skill5Format = "{スキル5}";
        public const string Skill6Format = "{スキル6}";
        public const string Skill7Format = "{スキル7}";
        public const string Skill8Format = "{スキル8}";

        public static string ToString(string format, IEnumerable<ScoreEntity> scores)
        {
            return string.Join(Environment.NewLine, scores.Select(x => ToString(format, x)));
        }

        public static string ToString(string format, ScoreEntity score)
        {
            return format
                .Replace(WarResultFormat, score.結果)
                .Replace(WarSideFormat, score.攻守)
                .Replace(OffenseCountryFormat, score.攻撃側国名)
                .Replace(DeffenseFormat, score.防衛側国名)
                .Replace(MapFormat, score.Map名)
                .Replace(WorkFormat, score.職業)
                .Replace(WarTimeFormat, score.戦争継続時間.ToString("mm\\:ss"))
                .Replace(BattleScoreFormat, score.戦闘)
                .Replace(RegionScoreFormat, score.領域)
                .Replace(SupportScoreFormat, score.支援)
                .Replace(PCDFormat, score.PC与ダメージ)
                .Replace(PCDKilloFormat, ToKillo(score.PC与ダメージ))
                .Replace(KillDamageBonusFormat, score.キルダメージボーナス)
                .Replace(SummonReleaseBonusFormat, score.召喚解除ボーナス)
                .Replace(BDFormat, score.建築与ダメージ)
                .Replace(BDKilloFormat, ToKillo(score.建築与ダメージ))
                .Replace(RegionDestroyBonusFormat, score.領域破壊ボーナス)
                .Replace(RegionDamageBonusFormat, score.領域ダメージボーナス)
                .Replace(ContributionScoreFormat, score.貢献度)
                .Replace(CrystalInvestmentBonusFormat, score.クリスタル運用ボーナス)
                .Replace(SummonBonusFormat, score.召喚行動ボーナス)
                .Replace(KillCountFormat, score.キル数)
                .Replace(DeadCountFormat, score.デッド数)
                .Replace(BuildCountFormat, score.建築数)
                .Replace(BuildDestroyCountFormat, score.建築物破壊数)
                .Replace(CrystalMiningFormat, score.クリスタル採掘量)
                .Replace(Skill1Format, score.スキル1)
                .Replace(Skill2Format, score.スキル2)
                .Replace(Skill3Format, score.スキル3)
                .Replace(Skill4Format, score.スキル4)
                .Replace(Skill5Format, score.スキル5)
                .Replace(Skill6Format, score.スキル6)
                .Replace(Skill7Format, score.スキル7)
                .Replace(Skill8Format, score.スキル8);
        }
    }

    public class AverageScoreTextFormatter : BaseScoreTextFormatter
    {
        public const string WarCountFormat = "{戦争数}";
        public const string WinRateFormat = "{勝率}";
        public const string AverageWarTimeFormat = "{平均戦争時間}";
        public const string AverageBattleScoreFormat = "{平均戦闘}";
        public const string AverageRegionScoreFormat = "{平均領域}";
        public const string AverageSupportScoreFormat = "{平均支援}";
        public const string AveragePCDFormat = "{平均PC与ダメージ}";
        public const string AveragePCDKilloFormat = "{平均PC与ダメージ_K}";
        public const string AverageKillDamageBonusFormat = "{平均キルダメージボーナス}";
        public const string AverageSummonReleaseBonusFormat = "{平均召喚解除ボーナス}";
        public const string AverageBDFormat = "{平均建築与ダメージ}";
        public const string AverageBDKilloFormat = "{平均建築与ダメージ_K}";
        public const string AverageRegionDestroyBonusFormat = "{平均領域破壊ボーナス}";
        public const string AverageRegionDamageBonusFormat = "{平均領域ダメージボーナス}";
        public const string AverageContributionScoreFormat = "{平均貢献度}";
        public const string AverageCrystalInvestmentBonusFormat = "{平均クリスタル運用ボーナス}";
        public const string AverageSummonBonusFormat = "{平均召喚行動ボーナス}";
        public const string AverageKillCountFormat = "{平均キル数}";
        public const string AverageDeadCountFormat = "{平均デッド数}";
        public const string AverageBuildCountFormat = "{平均建築数}";
        public const string AverageBuildDestroyCountFormat = "{平均建築物破壊数}";
        public const string AverageCrystalMiningFormat = "{平均クリスタル採掘量}";

        public static string ToString(string format, IEnumerable<ScoreEntity> scores)
        {
            var warCount = scores.Count();
            var winRate = Math.Round((double)scores.Count(x => x.結果 == WarResult.Win) / scores.Count() * 100, 1, MidpointRounding.AwayFromZero) + "%";
            var warTime = new TimeSpan((long)scores.Average(x => x.戦争継続時間.Ticks)).ToString("mm\\:ss");
            var battle = Math.Round(scores.Average(x => x.戦闘), 1, MidpointRounding.AwayFromZero);
            var region = Math.Round(scores.Average(x => x.領域), 1, MidpointRounding.AwayFromZero);
            var support = Math.Round(scores.Average(x => x.支援), 1, MidpointRounding.AwayFromZero);
            var pcd = Math.Round(scores.Average(x => x.PC与ダメージ), 1, MidpointRounding.AwayFromZero);
            var pcdKillo = ToKillo(Math.Round(scores.Average(x => x.PC与ダメージ), 1, MidpointRounding.AwayFromZero));
            var killDmg  = Math.Round(scores.Average(x => x.キルダメージボーナス), 1, MidpointRounding.AwayFromZero);
            var summonRelease = Math.Round(scores.Average(x => x.召喚解除ボーナス), 1, MidpointRounding.AwayFromZero);
            var bd = Math.Round(scores.Average(x => x.建築与ダメージ), 1, MidpointRounding.AwayFromZero);
            var bdKillo = ToKillo(Math.Round(scores.Average(x => x.建築与ダメージ), 1, MidpointRounding.AwayFromZero));
            var regionDestBns = Math.Round(scores.Average(x => x.領域破壊ボーナス), 1, MidpointRounding.AwayFromZero);
            var regionDmgBns = Math.Round(scores.Average(x => x.領域ダメージボーナス), 1, MidpointRounding.AwayFromZero);
            var contribution = Math.Round(scores.Average(x => x.貢献度), 1, MidpointRounding.AwayFromZero);
            var crystalInvBns = Math.Round(scores.Average(x => x.クリスタル運用ボーナス), 1, MidpointRounding.AwayFromZero);
            var summonBns = Math.Round(scores.Average(x => x.召喚行動ボーナス), 1, MidpointRounding.AwayFromZero);
            var kill = Math.Round(scores.Average(x => x.キル数), 1, MidpointRounding.AwayFromZero);
            var dead = Math.Round(scores.Average(x => x.デッド数), 1, MidpointRounding.AwayFromZero);
            var build = Math.Round(scores.Average(x => x.建築数), 1, MidpointRounding.AwayFromZero);
            var buildDst = Math.Round(scores.Average(x => x.建築物破壊数), 1, MidpointRounding.AwayFromZero);
            var crystalMining = Math.Round(scores.Average(x => x.クリスタル採掘量), 1, MidpointRounding.AwayFromZero);

            return format
                .Replace(WarCountFormat, warCount)
                .Replace(WinRateFormat, winRate)
                .Replace(AverageWarTimeFormat, warTime)
                .Replace(AverageBattleScoreFormat, battle)
                .Replace(AverageRegionScoreFormat, region)
                .Replace(AverageSupportScoreFormat, support)
                .Replace(AveragePCDFormat, pcd)
                .Replace(AveragePCDKilloFormat, pcdKillo)
                .Replace(AverageKillDamageBonusFormat, killDmg)
                .Replace(AverageSummonReleaseBonusFormat, summonRelease)
                .Replace(AverageBDFormat, bd)
                .Replace(AverageBDKilloFormat, bdKillo)
                .Replace(AverageRegionDestroyBonusFormat, regionDestBns)
                .Replace(AverageRegionDamageBonusFormat, regionDmgBns)
                .Replace(AverageContributionScoreFormat, contribution)
                .Replace(AverageCrystalInvestmentBonusFormat, crystalInvBns)
                .Replace(AverageSummonBonusFormat, summonBns)
                .Replace(AverageKillCountFormat, kill)
                .Replace(AverageDeadCountFormat, dead)
                .Replace(AverageBuildCountFormat, build)
                .Replace(AverageBuildDestroyCountFormat, buildDst)
                .Replace(AverageCrystalMiningFormat, crystalMining);
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
                nameof(ScoreEntity.建築与ダメージ), nameof(ScoreEntity.領域破壊ボーナス), nameof(ScoreEntity.領域ダメージボーナス),
                nameof(ScoreEntity.貢献度), nameof(ScoreEntity.クリスタル運用ボーナス), nameof(ScoreEntity.召喚行動ボーナス),
                nameof(ScoreEntity.キル数), nameof(ScoreEntity.デッド数), nameof(ScoreEntity.建築数), nameof(ScoreEntity.建築物破壊数),
                nameof(ScoreEntity.クリスタル採掘量), nameof(ScoreEntity.職業),
                nameof(ScoreEntity.スキル1), nameof(ScoreEntity.スキル2), nameof(ScoreEntity.スキル3),
                nameof(ScoreEntity.スキル4), nameof(ScoreEntity.スキル5), nameof(ScoreEntity.スキル6),
                nameof(ScoreEntity.スキル7), nameof(ScoreEntity.スキル8), nameof(ScoreEntity.備考));

            var values = scores.Select(x => string.Join(Delimiter,
                x.集計対象, x.記録日時, x.Map名, x.結果,
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

    public static class StringExtension
    {
        public static string Replace(this string own, string oldValue, object obj)
        {
            var newValue = (obj == null) ? string.Empty : obj.ToString();
            return own.Replace(oldValue, newValue);
        }
    }
}
