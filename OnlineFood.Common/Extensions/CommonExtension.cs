using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineFood.Common.Extensions
{
    public static class CommonExtension
    {
        public static Dictionary<string, object> ToDictionary(this object myObj)
        {
            return myObj.GetType()
                .GetProperties()
                .Select(pi => new { Name = pi.Name, Value = pi.GetValue(myObj, null) })
                .Union(
                    myObj.GetType()
                    .GetFields()
                    .Select(fi => new { Name = fi.Name, Value = fi.GetValue(myObj) })
                 )
                .ToDictionary(ks => ks.Name, vs => vs.Value);
        }

        public static string ToStringAttr(this Dictionary<string, object> myObj)
        {
            return string.Join(" ", myObj.Where(w => w.Value != null).Select(s => s.Key + "=\"" + s.Value.ToString() + "\"")).StringResolve();
        }

        public static string StringResolve(this string param)
        {
            return param == "" ? null : param;
        }

        public static string SetStringHtmlAttribute(this object htmlAttribute, string param = null)
        {
            var htmlatt = new Dictionary<string, object>();
            if (htmlAttribute != null)
            {
                htmlatt = htmlAttribute.ToDictionary();
            }
            if (htmlatt.Any(w => w.Key.ToLower() == "class"))
            {
                var found = htmlatt.FirstOrDefault(w => w.Key.ToLower() == "class");
                htmlatt.Remove(found.Key);
                htmlatt.Add("Class", found.Value + param);
            }
            else
            {
                htmlatt.Add("Class", param);
            }
            return htmlatt.ToStringAttr();
        }
        
        public static string CreateStringParams(this Dictionary<string, object> parameters)
        {
            return String.Join(",", parameters.ToList().Select(s => s.Key + ":" + s.Value).ToList());
        }

        public static object GetPropValue(this object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static IEnumerable<T> Except<T, TKey>(this IEnumerable<T> items, IEnumerable<T> other,
                                                                            Func<T, TKey> getKey)
        {
            return from item in items
                   join otherItem in other on getKey(item)
                   equals getKey(otherItem) into tempItems
                   from temp in tempItems.DefaultIfEmpty()
                   where ReferenceEquals(null, temp) || temp.Equals(default(T))
                   select item;

        }
    }
}
