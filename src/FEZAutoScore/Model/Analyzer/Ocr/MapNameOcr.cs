using FEZAutoScore.Extension;
using FEZAutoScore.Model.Entity;
using FEZAutoScore.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace FEZAutoScore.Model.Analyzer.Ocr
{
    public class MapNameOcr : StringOcr
    {
        private readonly Dictionary<string, byte[]> _mapDicionary;

        public MapNameOcr()
        {
            /* 
             * ResourcesファイルからMAP画像を取得し、Dictionaryに登録する。
             * @see https://msdn.microsoft.com/ja-jp/library/system.resources.resourcemanager.getresourceset(v=vs.110).aspx
             */
            // note: SkillOcr.csと定義が同じなため、共通化できるかも。
            var _works = Enum.GetNames(typeof(Work)).Where(x => x != nameof(Work.不明)).ToArray();
            // 判定:クラスのスキル画像以外なため、 _works.Anyの判定がSkillOcr.csと違う。
            _mapDicionary = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
                                    .Cast<DictionaryEntry>()
                                    .Select(x => new KeyValuePair<string, Bitmap>(x.Key.ToString(), x.Value as Bitmap))
                                    .Where(x => x.Value != null && !_works.Any(s => x.Key.StartsWith(s)))
                                    .ToDictionary(pair => pair.Key,
                                                  pair => pair.Value.GenerateHashFromBitmapData());
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
