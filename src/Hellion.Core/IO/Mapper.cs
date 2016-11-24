using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hellion.Core.IO
{
    public static class Mapper
    {
        public static void Map<T, U>(T source, U destination)
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                typeof(U)
                    .GetProperty(propertyInfo.Name,
                        BindingFlags.IgnoreCase |
                        BindingFlags.Instance |
                        BindingFlags.Public)
                    .SetValue(destination,
                        propertyInfo.GetValue(source));
            }
        }
    }
}
