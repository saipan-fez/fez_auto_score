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
    public class SkillOcr : StringOcr
    {
        private readonly Dictionary<string, byte[]> _skillDicionary;

        public SkillOcr()
        {
            var _works = Enum.GetNames(typeof(Work)).Where(x => x != nameof(Work.不明)).ToArray();
            /*
             * Resourcesファイルからクラスのスキル画像を取得し、Dictionaryに変換する。
             * @see https://msdn.microsoft.com/ja-jp/library/system.resources.resourcemanager.getresourceset(v=vs.110).aspx
             */
            _skillDicionary = Resources.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
                                    .Cast<DictionaryEntry>()
                                    .Select(x => new KeyValuePair<string, Bitmap>(x.Key.ToString(), x.Value as Bitmap))
                                    .Where(x => x.Value != null && _works.Any(s => x.Key.StartsWith(s)))
                                    .ToDictionary(pair => pair.Key,
                                                  pair => pair.Value.GenerateHashFromBitmapData());
        }

        protected override string Process(Bitmap bitmap)
        {
            // MD5を取得
            var hash = bitmap.GenerateHashFromBitmapData();

            // ハッシュ値が一致するスキル名を検索 (_S, _Dは選択・選択不可状態の画像のためスキル名からは削除)
            var res = _skillDicionary
                .FirstOrDefault(x => hash.SequenceEqual(x.Value)).Key;

            if (string.IsNullOrEmpty(res))
            {
                return null;
            }

            return res
                .Replace("Cestus_", "")
                .Replace("Fencer_", "")
                .Replace("Scout_", "")
                .Replace("Sorcerer_", "")
                .Replace("Warrior_", "")
                .Replace("_S", "")
                .Replace("_D", "");
        }

        public Work GetWork(string skillName)
        {
            var key = _skillDicionary.Keys.FirstOrDefault(x => x.IndexOf(skillName) != -1);

            if (string.IsNullOrEmpty(key))
            {
                return Work.不明;
            }

            if (key.IndexOf("Cestus_") != -1)
            {
                return Work.Cestus;
            }
            else if (key.IndexOf("Fencer_") != -1)
            {
                return Work.Fencer;
            }
            else if (key.IndexOf("Scout_") != -1)
            {
                return Work.Scout;
            }
            else if (key.IndexOf("Sorcerer_") != -1)
            {
                return Work.Sorcerer;
            }
            else if (key.IndexOf("Warrior_") != -1)
            {
                return Work.Warrior;
            }

            return Work.不明;
        }
    }
}
