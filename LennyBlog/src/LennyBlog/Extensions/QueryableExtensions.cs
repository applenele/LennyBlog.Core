using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LennyBlog.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName))
                return queryable;
            return QueryableHelper<T>.OrderBy(queryable, propertyName, false);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, bool desc)
        {
            if (string.IsNullOrEmpty(propertyName))
                return queryable;
            return QueryableHelper<T>.OrderBy(queryable, propertyName, desc);
        }

        /// <summary>
        /// 排序帮助
        /// </summary>
        /// <typeparam name="T"></typeparam>
        static class QueryableHelper<T>
        {
            public static IQueryable<T> OrderBy(IQueryable<T> queryable, string propertyName, bool desc)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
            }

            /// <summary>
            /// 构建表达式
            /// </summary>
            /// <param name="propertyName"></param>
            /// <returns></returns>
            private static LambdaExpression GetLambdaExpression(string propertyName)
            {
                var param = Expression.Parameter(typeof(T));
                var body = Expression.Property(param, propertyName);
                var keySelector = Expression.Lambda(body, param);
                return keySelector;
            }
        }
    }
}
