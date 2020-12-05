using System;
using System.Collections.Generic;
using System.Linq;

namespace valorant_stats
{
    public static class IEnumerableExtensions
    {
        public static T RandomElementByWeight<T>(this IEnumerable<T> list, Func<T, decimal> weightSelector)
        {
            decimal toGet = (decimal)new Random().NextDouble() * list.Sum(weightSelector), current = 0;

            foreach (var item in from weightedItem in list select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
            {
                current += item.Weight;

                if (current >= toGet)
                    return item.Value;
            }

            return default(T);
        }
    }
}
