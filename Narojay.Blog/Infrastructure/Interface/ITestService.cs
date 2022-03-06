using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Models.Dto;
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
        Task<bool> RedisLockTest();
        Task RedisLockTest1();
        Task<IdAndNameDto> GetData();
        Task<IdAndNameDto> GetDataException();
        Task<IdAndNameDto> GetDataException1();
    }
}
