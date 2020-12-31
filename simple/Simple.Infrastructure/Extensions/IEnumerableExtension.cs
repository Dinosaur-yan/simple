using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public static class IEnumerableExtension
{
    public static IEnumerable<T> ToDistinct<T>(this IEnumerable<T> source)
    {
        return source.Distinct(new TCompare<T>());
    }

    public static IEnumerable<T> ToDistinct<T, C>(this IEnumerable<T> source, Func<T, C> field)
    {
        return source.Distinct(new TCompare<T, C>(field));
    }

    public class TCompare<T> : IEqualityComparer<T>
    {
        public bool Equals(T x, T y)
        {
            try
            {
                bool result = true;
                PropertyInfo[] propertyInfos = typeof(T).GetProperties();
                foreach (var propertyInfo in propertyInfos)
                {
                    result = propertyInfo.GetValue(x, null)?.ToString()
                        == propertyInfo.GetValue(y, null)?.ToString();
                    if (!result) break;
                }
                return result;
            }
            catch
            {
                return false;
            }
        }

        public int GetHashCode(T obj)
        {
            return 1;
        }
    }

    public class TCompare<T, C> : IEqualityComparer<T>
    {
        private readonly Func<T, C> field;

        public TCompare(Func<T, C> field)
        {
            this.field = field;
        }

        public bool Equals(T x, T y)
        {
            return EqualityComparer<C>.Default.Equals(field(x), field(y));
        }

        public int GetHashCode(T obj)
        {
            return EqualityComparer<C>.Default.GetHashCode(field(obj));
        }
    }
}
