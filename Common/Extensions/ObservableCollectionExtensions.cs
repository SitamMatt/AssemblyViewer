using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Common.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> source, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                source.Add(item);
            }
        }

        public static void AddIfNotNull<T>(this ObservableCollection<T> source, T item)
        {
            if (item == null) return;
            source.Add(item);
        }
    }
}