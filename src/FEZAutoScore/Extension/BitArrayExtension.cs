using System.Collections;
using System.Linq;
using System.Text;

namespace FEZAutoScore.Extension
{
    static class BitArrayExtension
    {
        public static string ToBitString(this BitArray bits)
        {
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
            // ハミング距離を算出
            // ToDo: xor & popcountでも良いかも。
            if (source.Length != target.Length)
            {
                // hamming_thresholdで除外値にするためにMaxValueを設定
                return int.MaxValue;
            }
            var first = source.ToBitString();
            var second = target.ToBitString();
            return first.ToCharArray()
                .Zip(second.ToCharArray(), (c1, c2) => new { c1, c2 })
                .Count(m => m.c1 != m.c2);
        }
    }
}
