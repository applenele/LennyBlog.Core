using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LennyBlog.Extensions
{
    public static class EntityFrameworkExtensions
    { 

        public static int Update<T>(this IQueryable<T> query, Expression<Func<T, bool>> express)
        {
            int result = 0;

            return result;
        }
    }
}
