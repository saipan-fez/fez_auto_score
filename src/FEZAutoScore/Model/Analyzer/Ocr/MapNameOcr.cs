using FEZAutoScore.Extension;
using FEZAutoScore.Properties;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public class MapNameOcr : StringOcr
    {
        private Dictionary<string, byte[]> _mapDicionary;

        public MapNameOcr()
        {
            _mapDicionary = new Dictionary<string, byte[]>()
            {
                { nameof(Resources.アシロマ山麓), Resources.アシロマ山麓.GenerateHashFromBitmapData() },
                { nameof(Resources.アベル渓谷), Resources.アベル渓谷.GenerateHashFromBitmapData() },
                { nameof(Resources.アンバーステップ平原), Resources.アンバーステップ平原.GenerateHashFromBitmapData() },
                { nameof(Resources.アークトゥルス隕石跡), Resources.アークトゥルス隕石跡.GenerateHashFromBitmapData() },
                { nameof(Resources.インベイ高地), Resources.インベイ高地.GenerateHashFromBitmapData() },
                { nameof(Resources.ウィネッシュ渓谷), Resources.ウィネッシュ渓谷.GenerateHashFromBitmapData() },
                { nameof(Resources.ウェンズデイ古戦場跡), Resources.ウェンズデイ古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.ウォーロック古戦場跡), Resources.ウォーロック古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.エルギル高原), Resources.エルギル高原.GenerateHashFromBitmapData() },
                { nameof(Resources.オブシディアン荒地), Resources.オブシディアン荒地.GenerateHashFromBitmapData() },
                { nameof(Resources.オリオン廃街), Resources.オリオン廃街.GenerateHashFromBitmapData() },
                { nameof(Resources.カペラ隕石跡), Resources.カペラ隕石跡.GenerateHashFromBitmapData() },
                { nameof(Resources.キンカッシュ古戦場跡), Resources.キンカッシュ古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.クダン丘陵), Resources.クダン丘陵.GenerateHashFromBitmapData() },
                { nameof(Resources.クノーラ雪原), Resources.クノーラ雪原.GenerateHashFromBitmapData() },
                { nameof(Resources.クラウス山脈), Resources.クラウス山脈.GenerateHashFromBitmapData() },
                { nameof(Resources.クローディア水源), Resources.クローディア水源.GenerateHashFromBitmapData() },
                { nameof(Resources.グランフォーク河口), Resources.グランフォーク河口.GenerateHashFromBitmapData() },
                { nameof(Resources.ゴブリンフォーク), Resources.ゴブリンフォーク.GenerateHashFromBitmapData() },
                { nameof(Resources.ザーク古戦場跡), Resources.ザーク古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.シディット水域), Resources.シディット水域.GenerateHashFromBitmapData() },
                { nameof(Resources.シバーグ遺跡), Resources.シバーグ遺跡.GenerateHashFromBitmapData() },
                { nameof(Resources.シュア島古戦場跡), Resources.シュア島古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.ジャコル丘陵), Resources.ジャコル丘陵.GenerateHashFromBitmapData() },
                { nameof(Resources.スピカ隕石跡), Resources.スピカ隕石跡.GenerateHashFromBitmapData() },
                { nameof(Resources.セノビア荒地), Resources.セノビア荒地.GenerateHashFromBitmapData() },
                { nameof(Resources.セルベーン高地), Resources.セルベーン高地.GenerateHashFromBitmapData() },
                { nameof(Resources.セントウォーク高地), Resources.セントウォーク高地.GenerateHashFromBitmapData() },
                { nameof(Resources.ソーン平原), Resources.ソーン平原.GenerateHashFromBitmapData() },
                { nameof(Resources.タマライア水源), Resources.タマライア水源.GenerateHashFromBitmapData() },
                { nameof(Resources.ダガー島), Resources.ダガー島.GenerateHashFromBitmapData() },
                { nameof(Resources.デスパイア山麓), Resources.デスパイア山麓.GenerateHashFromBitmapData() },
                { nameof(Resources.ドランゴラ荒地), Resources.ドランゴラ荒地.GenerateHashFromBitmapData() },
                { nameof(Resources.ニコナ街道), Resources.ニコナ街道.GenerateHashFromBitmapData() },
                { nameof(Resources.ネフタル雪原), Resources.ネフタル雪原.GenerateHashFromBitmapData() },
                { nameof(Resources.ノイム草原), Resources.ノイム草原.GenerateHashFromBitmapData() },
                { nameof(Resources.フェブェ雪原), Resources.フェブェ雪原.GenerateHashFromBitmapData() },
                { nameof(Resources.ブリザール湿原), Resources.ブリザール湿原.GenerateHashFromBitmapData() },
                { nameof(Resources.ブローデン古戦場跡), Resources.ブローデン古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.ヘイムダル荒地), Resources.ヘイムダル荒地.GenerateHashFromBitmapData() },
                { nameof(Resources.ベルタ平原), Resources.ベルタ平原.GenerateHashFromBitmapData() },
                { nameof(Resources.ホークウィンド高地), Resources.ホークウィンド高地.GenerateHashFromBitmapData() },
                { nameof(Resources.マスクス水源), Resources.マスクス水源.GenerateHashFromBitmapData() },
                { nameof(Resources.ラインレイ渓谷), Resources.ラインレイ渓谷.GenerateHashFromBitmapData() },
                { nameof(Resources.ラナス城跡), Resources.ラナス城跡.GenerateHashFromBitmapData() },
                { nameof(Resources.ルダン雪原), Resources.ルダン雪原.GenerateHashFromBitmapData() },
                { nameof(Resources.ルード雪原), Resources.ルード雪原.GenerateHashFromBitmapData() },
                { nameof(Resources.レイクパス荒地), Resources.レイクパス荒地.GenerateHashFromBitmapData() },
                { nameof(Resources.ログマール古戦場跡), Resources.ログマール古戦場跡.GenerateHashFromBitmapData() },
                { nameof(Resources.ロザリオ高地), Resources.ロザリオ高地.GenerateHashFromBitmapData() },
                { nameof(Resources.ロッシ雪原), Resources.ロッシ雪原.GenerateHashFromBitmapData() },
                { nameof(Resources.ローグローブ台地), Resources.ローグローブ台地.GenerateHashFromBitmapData() },
                { nameof(Resources.ワーグノスの地), Resources.ワーグノスの地.GenerateHashFromBitmapData() },
                { nameof(Resources.ワードノール平原), Resources.ワードノール平原.GenerateHashFromBitmapData() },
                { nameof(Resources.始まりの大地), Resources.始まりの大地.GenerateHashFromBitmapData() },
            };
        }

        protected override string Process(Bitmap bitmap)
        {
            // bitmapを2値化
            bitmap.ToThresholding(true);

            // ハッシュ値を取得
            var hash = bitmap.GenerateHashFromBitmapData();

            // ハッシュ値が一致するMAP名を検索
            return _mapDicionary
                .FirstOrDefault(x => hash.SequenceEqual(x.Value)).Key;
        }
    }
}
