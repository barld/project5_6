using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Helpers
{
    public static class CollectionExtensions
    {
        public static IEnumerable<U> PickWhereType<T,U>(this IEnumerable<T> collection) where U : class
        {
            return collection.Where(item => item is U).Select(item => item as U);
        }
    }
}
