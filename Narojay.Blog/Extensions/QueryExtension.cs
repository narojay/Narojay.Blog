using System;
using System.Linq;
using System.Linq.Expressions;

namespace Narojay.Blog.Extensions
{
    public static class QueryExtension
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> func)
        {
            return condition ? query : query.Where(func);
        }
    }
}