using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Base.WpfBase
{
    public static class ObservableExtension
    {
        public static void Sort<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }

        public static void Sort<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keySelector,bool isdesc)
        {
            List<TSource> sortedList = isdesc? source.OrderByDescending(keySelector).ToList(): source.OrderBy(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }

        public static void SortDesc<TSource, TKey>(this Collection<TSource> source, Func<TSource, TKey> keySelector)
        {
            List<TSource> sortedList = source.OrderByDescending(keySelector).ToList();
            source.Clear();
            foreach (var sortedItem in sortedList)
                source.Add(sortedItem);
        }

        public static void Sort<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sortedList = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sortedList.Count(); i++)
            {
                collection.Move(collection.IndexOf(sortedList[i]), i);
            }
        }
    }
}
