using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface ITestService
    {
        Task GenerateInitData();
        Task GetTestPage();

        Task UpdateTest();
        Task InsertLog(Elog elog);
        Task UpdateLog(Elog elog);
        Task<Tuple<int, IList<Elog>>> QueryLog(int page, int limit);
        Task<Elog> QueryLog(int id);
        Task DeleteLog(int id );
    }
}
