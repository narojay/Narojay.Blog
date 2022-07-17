using System.Linq;

namespace Narojay.Blog.Infrastruct.Extension;

public static class QueryableExtension
{
    public static IQueryable<T> PageBy<T>(this IQueryable<T> queryable, int pageIndex, int pageSize)
    {
        return queryable.Skip((pageIndex - 1) * (pageSize)).Take(pageSize);
    }
}