namespace Snake
{
    using System;
    using System.Collections.Generic;

    public static class Extensions
    {
        public static void ForEach<TElem>(this IEnumerable<TElem> source, Action<TElem> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static HashSet<TElem> ToHashSet<TElem>(this IEnumerable<TElem> source) => new HashSet<TElem>(source);
    }
}