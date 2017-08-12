using System;
using System.Collections.Generic;
using System.Linq;

namespace ConspectoPatronum.Core.Extensions
{
    public static class ExtensionMethods
    {
        public static IEnumerable<T> TopologicalSort<T>(this IQueryable<T> items,
            Func<T, T> previousItemSelector, Func<T, T> nextItemSelector)
        {
            var item = items.First();
            while (previousItemSelector(item) != null)
            {
                item = previousItemSelector(item);
            }
            while (item != null)
            {
                yield return item;
                item = nextItemSelector(item);
            }
        }

        public static T GetSink<T>(this IEnumerable<T> items, Func<T, T> nextItemSelector)
        {
            var item = items.Last();
            while (nextItemSelector(item) != null)
            {
                item = nextItemSelector(item);
            }
            return item;
        }
    }
}
