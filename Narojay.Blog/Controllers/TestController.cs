using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{

    [Route("test")]
    public class TestController : BaseController
    {

        public ITestService TestService { get; set; }
        [HttpPost("test")]
        public Task GenerateInitData(string username, string password) => TestService.GenerateInitData();


        [HttpPost("page")]
        public Task GetTestPage(string username, string password) => TestService.GetTestPage();

        [HttpPost("update")]
        public Task updateTest() => TestService.UpdateTest();

        [HttpGet("elog/id={id}")]
        public Task<Elog> GetElogById(int id) => TestService.QueryLog(id);

        [HttpPost("elog/page={page}")]
        public Task<Tuple<int, IList<Elog>>> GetElogs(int page, int limit) => TestService.QueryLog(page, limit);
        [HttpPost("elog/add")]
        public Task InsertElogs(Elog elog) => TestService.InsertLog(elog);

        [HttpPost("redislock")]
        public Task RedisLockTest(Elog elog) => TestService.RedisLockTest();

        [HttpPost("redislock1")]
        public Task RedisLockTest1() => TestService.RedisLockTest1();
    }
}
