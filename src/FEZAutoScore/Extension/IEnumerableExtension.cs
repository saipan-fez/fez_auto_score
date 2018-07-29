using System.Collections.Generic;
using System.Linq;

namespace FEZAutoScore.Extension
{
    static class IEnumerableExtension
    {
        public static IEnumerable<IReadOnlyList<T>> Chunks<T>(this IEnumerable<T> source, int size)
        {
            /*
             * N個ずつの要素に分割
             */
            while (source.Any())
            {
                yield return new List<T>(source.Take(size));
                source = source.Skip(size);
            }
        }
    }
}
