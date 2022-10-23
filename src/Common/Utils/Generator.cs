using System;
using System.Collections.Generic;

namespace CodeEfficiencyBenchmark.Common.Utils
{
    public static class Generator
    {
        public static IEnumerable<int> EnumerableRange(int start, int count)
        {
            long num = (long)start + (long)count - 1;

            if (count < 0 || num > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            return RangeIterator(start, count);
        }

        private static IEnumerable<int> RangeIterator(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return start + i;
            }
        }
    }
}
