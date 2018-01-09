using FEZAutoScore.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FEZAutoScore.Model.Entity
{
    public enum WarResult
    {
        Win,
        Lose
    }

    public enum Work
    {
        不明,
        Warrior,
        Scout,
        Sorcerer,
        Fencer,
        Cestus,
    }

    [Table("Score")]
    public class ScoreEntity : BindableBase
    {
        [Required]
        public bool 集計対象 { get { return _集計対象; } set { SetProperty(ref _集計対象, value); } }
        private bool _集計対象 = true;

        [Key]
        [Required]
        public DateTime 記録日時 { get { return _記録日時; } set { SetProperty(ref _記録日時, value); } }
        private DateTime _記録日時;

        [Required]
        public string Map名 { get { return _Map名; } set { SetProperty(ref _Map名, value); } }
        private string _Map名;

        [Required]
        public WarResult 結果 { get { return _結果; } set { SetProperty(ref _結果, value); } }
        private WarResult _結果;
        [Required]
        public TimeSpan 戦争継続時間 { get { return _戦争継続時間; } set { SetProperty(ref _戦争継続時間, value); } }
        private TimeSpan _戦争継続時間;

        [Required]
        public int 戦闘 { get { return _戦闘; } set { SetProperty(ref _戦闘, value); } }
        private int _戦闘;
        [Required]
        public int 領域 { get { return _領域; } set { SetProperty(ref _領域, value); } }
        private int _領域;
        [Required]
        public int 支援 { get { return _支援; } set { SetProperty(ref _支援, value); } }
        private int _支援;

        [Required]
        public int PC与ダメージ { get { return _PC与ダメージ; } set { SetProperty(ref _PC与ダメージ, value); } }
        private int _PC与ダメージ;
        [Required]
        public int キルダメージボーナス { get { return _キルダメージボーナス; } set { SetProperty(ref _キルダメージボーナス, value); } }
        private int _キルダメージボーナス;
        [Required]
        public int 召喚解除ボーナス { get { return _召喚解除ボーナス; } set { SetProperty(ref _召喚解除ボーナス, value); } }
        private int _召喚解除ボーナス;

        [Required]
        public int 建築与ダメージ { get { return _建築与ダメージ; } set { SetProperty(ref _建築与ダメージ, value); } }
        private int _建築与ダメージ;
        [Required]
        public int 領域破壊ボーナス { get { return _領域破壊ボーナス; } set { SetProperty(ref _領域破壊ボーナス, value); } }
        private int _領域破壊ボーナス;
        [Required]
        public int 領域ダメージボーナス { get { return _領域ダメージボーナス; } set { SetProperty(ref _領域ダメージボーナス, value); } }
        private int _領域ダメージボーナス;

        [Required]
        public int 貢献度 { get { return _貢献度; } set { SetProperty(ref _貢献度, value); } }
        private int _貢献度;
        [Required]
        public int クリスタル運用ボーナス { get { return _クリスタル運用ボーナス; } set { SetProperty(ref _クリスタル運用ボーナス, value); } }
        private int _クリスタル運用ボーナス;
        [Required]
        public int 召喚行動ボーナス { get { return _召喚行動ボーナス; } set { SetProperty(ref _召喚行動ボーナス, value); } }
        private int _召喚行動ボーナス;

        [Required]
        public int キル数 { get { return _キル数; } set { SetProperty(ref _キル数, value); } }
        private int _キル数;
        [Required]
        public int デッド数 { get { return _デッド数; } set { SetProperty(ref _デッド数, value); } }
        private int _デッド数;
        [Required]
        public int 建築数 { get { return _建築数; } set { SetProperty(ref _建築数, value); } }
        private int _建築数;
        [Required]
        public int 建築物破壊数 { get { return _建築物破壊数; } set { SetProperty(ref _建築物破壊数, value); } }
        private int _建築物破壊数;
        [Required]
        public int クリスタル採掘量 { get { return _クリスタル採掘量; } set { SetProperty(ref _クリスタル採掘量, value); } }
        private int _クリスタル採掘量;

        [Required]
        public Work 職業 { get { return _職業; } set { SetProperty(ref _職業, value); } }
        private Work _職業;

        [Required]
        public string スキル1 { get { return _スキル1; } set { SetProperty(ref _スキル1, value); } }
        private string _スキル1;
        [Required]
        public string スキル2 { get { return _スキル2; } set { SetProperty(ref _スキル2, value); } }
        private string _スキル2;
        [Required]
        public string スキル3 { get { return _スキル3; } set { SetProperty(ref _スキル3, value); } }
        private string _スキル3;
        [Required]
        public string スキル4 { get { return _スキル4; } set { SetProperty(ref _スキル4, value); } }
        private string _スキル4;
        [Required]
        public string スキル5 { get { return _スキル5; } set { SetProperty(ref _スキル5, value); } }
        private string _スキル5;
        [Required]
        public string スキル6 { get { return _スキル6; } set { SetProperty(ref _スキル6, value); } }
        private string _スキル6;
        [Required]
        public string スキル7 { get { return _スキル7; } set { SetProperty(ref _スキル7, value); } }
        private string _スキル7;
        [Required]
        public string スキル8 { get { return _スキル8; } set { SetProperty(ref _スキル8, value); } }
        private string _スキル8;

        public string 備考 { get { return _備考; } set { SetProperty(ref _備考, value); } }
        private string _備考;
    }
}
