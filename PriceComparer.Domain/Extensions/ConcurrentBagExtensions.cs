using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PriceComparer.Domain
{
    public static class ConcurrentBagExtensions
    {
        public static void AddRange<T>(this ConcurrentBag<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }
    }
}
