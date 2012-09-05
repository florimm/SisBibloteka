using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Utilities
{
    public static class EnumExtensions
    {
        public static Dictionary<int, string> ToDistionary(this Type enumType)
        {
            return enumType.ToDistionary(false);
        }
        public static Dictionary<int, string> ToDistionary(this Type enumType, bool dummyData)
        {
            if (!enumType.IsEnum)
                throw new ApplicationException("GetListItems does not support non-enum types");
            var list = new Dictionary<int, string>();
            foreach (FieldInfo field in enumType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public))
            {
                var value = (int)field.GetValue(null);
                var display = Enum.GetName(enumType, value);
                //list.Add(value, display.SplitCapitalize());
                list.Add(value, display);
            }
            if (dummyData)
            {
                list.Add(-1, "Other");
            }
            return list;
        }
        public static Dictionary<int, string> ToDictionary<T>(this IEnumerable<T> source, Func<T, int> key, Func<T, string> val, bool dummyData)
        {
            var dic = source.ToDictionary(key, val);
            if (dummyData)
            {
                dic.Add(-1, "Other");
            }
            return dic;
        }
        public static Dictionary<int, string> ToDictionary<T>(this IQueryable<T> source, Func<T, int> key, Func<T, string> val, bool dummyData)
        {
            var dic = source.ToDictionary(key, val);
            if (dummyData)
            {
                dic.Add(-1, "Other");
            }
            return dic;
        }
    }

    public static class QueryableExtensions
    {
        public static ObservableCollection<T> ToObservable<T>(this IQueryable<T> source) where T : class
        {
            var data = new ObservableCollection<T>(source.ToList());
            return data;
        }
    }
}
