using System.Collections;
using System.Linq;
using System.Text;

namespace FEZAutoScore.Extension
{
    static class BitArrayExtension
    {
        public static string ToBitString(this BitArray bits)
        {
            /*
             * 2進数文字列に変換
             */
            var sb = new StringBuilder(bits.Count);
            for (var i = 0; i < bits.Count; i++)
            {
                char c = bits[i] ? '1' : '0';
                sb.Append(c);
            }
            return sb.ToString();
        }

        public static int GetHammingDistance(this BitArray source, BitArray target)
        {
            /*
             * ハミング距離を算出
             * note: ビットが立っている判定はpopcountでも良いかも。
             */
            if (source.Length != target.Length)
            {
                // hamming_thresholdで除外値にするためにMaxValueを返す。
                return int.MaxValue;
            }
            var copyed = new BitArray(source).Xor(target);
            int count = 0;
            for (int i = 0; i < copyed.Length; i++)
            {
                if (copyed[i])
                {
                    count++;
                }
            }
            return count;
        }
    }
}
