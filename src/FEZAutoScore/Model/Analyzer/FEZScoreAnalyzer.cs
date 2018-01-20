using FEZAutoScore.Model.Analyzer.Ocr;
using FEZAutoScore.Model.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace FEZAutoScore.Model.Analyzer
{
    public class FEZScoreAnalyzer
    {
        // 戦績結果ウィンドウのサイズ
        private readonly Size ResultWindowSize = new Size(747, 706);

        // 画像の中心点から戦績結果ウィンドウ左上の相対座標
        private readonly Point ResultWindowLeftTopPoint = new Point(-262, -353);

        // FEZの戦績結果ウィンドウかどうかの判定に用いる座標および色
        private readonly Dictionary<Point, Color> ResultCheckTable = new Dictionary<Point, Color>()
        {
            { new Point(20, 200),  Color.FromArgb(50, 50, 50) },
            { new Point(50, 200),  Color.FromArgb(167, 155, 145) },
            { new Point(730, 335), Color.FromArgb(50, 50, 50) },
            { new Point(730, 550), Color.FromArgb(61, 47, 43) },
        };

        // 戦争結果判定用の座標と色
        private readonly Point WarResultPoint = new Point(250, 50);
        private readonly Color WarResultLoseColor = Color.FromArgb(52, 52, 52);

        // 攻守判定用の座標と色
        private readonly Point WarSidePoint = new Point(200, 100);
        private readonly Color WarSideOffenseColor = Color.FromArgb(208, 199, 178);

        // 国名判定用の座標と国名・色
        private readonly Point DefenseCountryPoint = new Point(100, 90);
        private readonly Point OffenseCountryPoint = new Point(414, 90);
        private readonly Dictionary<Color, string> CountryTable = new Dictionary<Color, string>()
        {
            { Color.FromArgb(54,  105, 255), "エルソード王国" },
            { Color.FromArgb(86,  231,  37), "カセドリア連合王国" },
            { Color.FromArgb(161,  39, 196), "ゲブランド帝国" },
            { Color.FromArgb(48,   44,  42), "ネツァワル王国" },
            { Color.FromArgb(250, 222,  45), "ホルデイン王国" },
        };

        // 各スコアが描かれている位置
        private readonly Dictionary<string, Rectangle> ScoreRectTable = new Dictionary<string, Rectangle>()
        {
            // MAP名
            { nameof(ScoreEntity.Map名),                  new Rectangle(103, 10, 160, 11) },

            // 戦争継続時間
            { nameof(ScoreEntity.戦争継続時間),           new Rectangle(105, 273, 314, 9) },

            // 合計スコア
            { nameof(ScoreEntity.戦闘),                   new Rectangle(325, 469, 60, 30) },
            { nameof(ScoreEntity.領域),                   new Rectangle(325, 497, 60, 30) },
            { nameof(ScoreEntity.支援),                   new Rectangle(325, 525, 60, 30) },

            // 詳細スコア
            { nameof(ScoreEntity.PC与ダメージ),           new Rectangle(677, 349, 50, 9) },
            { nameof(ScoreEntity.キルダメージボーナス),   new Rectangle(677, 364, 50, 9) },
            { nameof(ScoreEntity.召喚解除ボーナス) ,      new Rectangle(677, 379, 50, 9) },
            { nameof(ScoreEntity.建築与ダメージ),         new Rectangle(677, 409, 50, 9) },
            { nameof(ScoreEntity.領域破壊ボーナス),       new Rectangle(677, 424, 50, 9) },
            { nameof(ScoreEntity.領域ダメージボーナス),   new Rectangle(677, 439, 50, 9) },
            { nameof(ScoreEntity.貢献度),                 new Rectangle(677, 469, 50, 9) },
            { nameof(ScoreEntity.クリスタル運用ボーナス), new Rectangle(677, 484, 50, 9) },
            { nameof(ScoreEntity.召喚行動ボーナス) ,      new Rectangle(677, 499, 50, 9) },

            // 各カウント
            { nameof(ScoreEntity.キル数) ,                new Rectangle(170, 568, 60, 16) },
            { nameof(ScoreEntity.デッド数) ,              new Rectangle(170, 584, 60, 16) },
            { nameof(ScoreEntity.建築数) ,                new Rectangle(170, 600, 60, 16) },
            { nameof(ScoreEntity.建築物破壊数) ,          new Rectangle(170, 616, 60, 16) },
            { nameof(ScoreEntity.クリスタル採掘量) ,      new Rectangle(170, 632, 60, 16) },
        };

        // 各スキルが描かれている位置 (x座標のみ画像右側からの相対座標)
        private readonly Dictionary<string, Rectangle> SkillRectTable = new Dictionary<string, Rectangle>()
        {
            { nameof(ScoreEntity.スキル1), new Rectangle(-30, 22, 12, 12) },
            { nameof(ScoreEntity.スキル2), new Rectangle(-30, 54, 12, 12) },
            { nameof(ScoreEntity.スキル3), new Rectangle(-30, 86, 12, 12) },
            { nameof(ScoreEntity.スキル4), new Rectangle(-30, 118, 12, 12) },
            { nameof(ScoreEntity.スキル5), new Rectangle(-30, 150, 12, 12) },
            { nameof(ScoreEntity.スキル6), new Rectangle(-30, 182, 12, 12) },
            { nameof(ScoreEntity.スキル7), new Rectangle(-30, 214, 12, 12) },
            { nameof(ScoreEntity.スキル8), new Rectangle(-30, 246, 12, 12) },
        };

        WarTimeOcr _warTimeOcr = new WarTimeOcr();
        TotalScoreOcr _totalOcr = new TotalScoreOcr();
        DetailScoreOcr _detailOcr = new DetailScoreOcr();
        KillScoreOcr _killOcr = new KillScoreOcr();
        MapNameOcr _mapNameOcr = new MapNameOcr();
        SkillOcr _skillOcr = new SkillOcr();

        public ScoreEntity Analyze(Bitmap screenBitmap)
        {
            /*
             * 解析のフロー
             * 1. 画像から戦績結果ウィンドウが表示されているはずの領域を切り抜く
             *      → スコア画面は解像度に依存せず、一定の大きさで、なおかつ画面中央に表示される。
             *         そのため、画像の中心座標から一定の範囲を切り抜いて以降で用いる。
             *
             * 2. FEZ画面かどうかをチェックする
             *      → 切り抜いた画像の特定の座標が想定される色かどうかチェックする。
             *         この後に行うOCRは処理が重いため、戦績結果ウィンドウではない画像をこの時点で弾く。
             *         (もし偶然このチェックが通ってしまった場合はOCRの結果から判断する)
             *
             * 3. OCR実行
             *      → スコア表示部分はフォントが異なるため、それぞれのフォント向けの解析処理クラスを用いて解析を実施する。
             *         解析処理クラスのアルゴリズムについては /doc/OCR_Algorithm.md を参照のこと。
             */

            // 1. 画像から戦績結果ウィンドウが表示されているはずの領域を切り抜く
            using (var bitmap = ClipResult(screenBitmap))
            {
                // 2. FEZ画面かどうかをチェック
                if (!IsFezResultWindow(bitmap))
                {
                    return null;
                }

                ScoreEntity ret = null;

                // 3. OCR実行
                try
                {
                    var score = new ScoreEntity();

                    // マップ名 (画像は切り抜く前の画像)
                    score.Map名 = Scan(_mapNameOcr, screenBitmap, ScoreRectTable[nameof(ScoreEntity.Map名)]) ?? "不明";

                    // 時間
                    score.記録日時 = DateTime.Now;

                    // 戦争結果
                    score.結果 = ScanWarResult(bitmap);

                    // 攻守
                    score.攻守 = ScanWarSide(bitmap);

                    // 国名
                    score.攻撃側国名 = ScanCountry(OffenseCountryPoint, bitmap);
                    score.防衛側国名 = ScanCountry(DefenseCountryPoint, bitmap);

                    // 戦争経過時間
                    score.戦争継続時間 = Scan(_warTimeOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.戦争継続時間)]);

                    // 合計スコア
                    score.戦闘 = Scan(_totalOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.戦闘)]);
                    score.領域 = Scan(_totalOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.領域)]);
                    score.支援 = Scan(_totalOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.支援)]);

                    // 詳細スコア
                    score.PC与ダメージ = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.PC与ダメージ)]);
                    score.キルダメージボーナス = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.キルダメージボーナス)]);
                    score.召喚解除ボーナス = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.召喚解除ボーナス)]);
                    score.建築与ダメージ = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.建築与ダメージ)]);
                    score.領域破壊ボーナス = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.領域破壊ボーナス)]);
                    score.領域ダメージボーナス = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.領域ダメージボーナス)]);
                    score.貢献度 = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.貢献度)]);
                    score.クリスタル運用ボーナス = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.クリスタル運用ボーナス)]);
                    score.召喚行動ボーナス = Scan(_detailOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.召喚行動ボーナス)]);

                    // キル数など
                    score.キル数 = Scan(_killOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.キル数)]);
                    score.デッド数 = Scan(_killOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.デッド数)]);
                    score.建築数 = Scan(_killOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.建築数)]);
                    score.建築物破壊数 = Scan(_killOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.建築物破壊数)]);
                    score.クリスタル採掘量 = Scan(_killOcr, bitmap, ScoreRectTable[nameof(ScoreEntity.クリスタル採掘量)]);

                    // スキル (画像は切り抜く前の画像)
                    score.スキル1 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル1)]) ?? "不明";
                    score.スキル2 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル2)]) ?? "不明";
                    score.スキル3 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル3)]) ?? "不明";
                    score.スキル4 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル4)]) ?? "不明";
                    score.スキル5 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル5)]) ?? "不明";
                    score.スキル6 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル6)]) ?? "不明";
                    score.スキル7 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル7)]) ?? "不明";
                    score.スキル8 = ScanSkill(screenBitmap, SkillRectTable[nameof(ScoreEntity.スキル8)]) ?? "不明";

                    // 職業
                    score.職業 = ScanWork(
                        score.スキル1, score.スキル2, score.スキル3, score.スキル4,
                        score.スキル5, score.スキル6, score.スキル7, score.スキル8);

                    ret = score;
                }
                catch (OcrFailedException)
                { }
                catch
                {
                    throw;
                }

                return ret;
            }
        }

        private bool IsFezResultWindow(Bitmap bitmap)
        {
            bool ret = true;

            foreach (var c in ResultCheckTable)
            {
                ret &= (bitmap.GetPixel(c.Key.X, c.Key.Y) == c.Value);
            }

            return ret;
        }

        private Bitmap ClipResult(Bitmap bitmap)
        {
            // FEZのスコア画面は解像度に依存せず、一定の大きさである。
            // また、画面中央に表示されるため、中心から一定の範囲を切り抜いて、
            // 以降に扱う画像サイズを一定にする。

            // 画像の中心点
            Point center = new Point(
                bitmap.Size.Width / 2,
                bitmap.Size.Height / 2);

            // 切り抜く左上の座標
            Point clipleftTop = new Point(
                center.X + ResultWindowLeftTopPoint.X,
                center.Y + ResultWindowLeftTopPoint.Y);

            return bitmap.Clone(new Rectangle(clipleftTop, ResultWindowSize), PixelFormat.Format24bppRgb);
        }

        private string ScanSkill(Bitmap bitmap, Rectangle r)
        {
            Rectangle absRect = r;
            absRect.X = bitmap.Width + r.X;

            return Scan(_skillOcr, bitmap, absRect);
        }

        private T Scan<T>(Ocr<T> ocr, Bitmap bitmap, Rectangle r)
        {
            var result = ocr.Process(bitmap, r);

            return result;
        }

        private WarResult ScanWarResult(Bitmap bitmap)
        {
            var color = bitmap.GetPixel(WarResultPoint.X, WarResultPoint.Y);

            return color != WarResultLoseColor ? WarResult.Win : WarResult.Lose;
        }

        private string ScanWarSide(Bitmap bitmap)
        {
            var color = bitmap.GetPixel(WarSidePoint.X, WarSidePoint.Y);

            return color == WarSideOffenseColor ? "攻撃" : "防衛";
        }

        private string ScanCountry(Point point, Bitmap bitmap)
        {
            var color = bitmap.GetPixel(point.X, point.Y);

            return CountryTable.TryGetValue(color, out string country) ? country : "不明";
        }

        private Work ScanWork(
            string スキル1, string スキル2, string スキル3, string スキル4,
            string スキル5, string スキル6, string スキル7, string スキル8)
        {
            var s1 = _skillOcr.GetWork(スキル1);
            if (s1 != Work.不明)
            {
                return s1;
            }
            var s2 = _skillOcr.GetWork(スキル2);
            if (s2 != Work.不明)
            {
                return s1;
            }
            var s3 = _skillOcr.GetWork(スキル3);
            if (s3 != Work.不明)
            {
                return s1;
            }
            var s4 = _skillOcr.GetWork(スキル4);
            if (s4 != Work.不明)
            {
                return s1;
            }
            var s5 = _skillOcr.GetWork(スキル5);
            if (s5 != Work.不明)
            {
                return s1;
            }
            var s6 = _skillOcr.GetWork(スキル6);
            if (s6 != Work.不明)
            {
                return s1;
            }
            var s7 = _skillOcr.GetWork(スキル7);
            if (s7 != Work.不明)
            {
                return s1;
            }
            var s8 = _skillOcr.GetWork(スキル8);
            if (s8 != Work.不明)
            {
                return s1;
            }

            return Work.不明;
        }
    }
}
