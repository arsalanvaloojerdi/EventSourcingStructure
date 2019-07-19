using System;
using System.Collections.Generic;

namespace Framework.Core.Extentions
{
    public static class CollectionExtentions
    {
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> contents)
        {
            foreach (var content in contents)
            {
                destination.Add(content);
            }
        }

        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}