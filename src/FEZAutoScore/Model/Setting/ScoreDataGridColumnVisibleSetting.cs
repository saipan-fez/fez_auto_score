using Reactive.Bindings;

namespace FEZAutoScore.Model.Setting
{
    public class ScoreDataGridColumnVisibleSetting : BaseSetting
    {
        public ReactiveProperty<bool> 集計対象 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> 記録日時 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> Map名 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> 結果 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> 戦争継続時間 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 戦闘 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 領域 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 支援 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> PC与ダメージ { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> キルダメージボーナス { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 召喚解除ボーナス { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 建築与ダメージ { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> 領域破壊ボーナス { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 領域ダメージボーナス { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 貢献度 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> クリスタル運用ボーナス { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 召喚行動ボーナス { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> キル数 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> デッド数 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> 建築数 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 建築物破壊数 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> クリスタル採掘量 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 職業 { get; } = new ReactiveProperty<bool>(true);
        public ReactiveProperty<bool> スキル1 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル2 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル3 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル4 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル5 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル6 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル7 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> スキル8 { get; } = new ReactiveProperty<bool>();
        public ReactiveProperty<bool> 備考 { get; } = new ReactiveProperty<bool>(true);

        public ScoreDataGridColumnVisibleSetting()
        {
        }
    }
}
