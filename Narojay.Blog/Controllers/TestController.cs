using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
    [Route("test")]
    public class TestController : BaseController
    {
        public ITestService TestService { get; set; }

        [HttpPost("test")]
        public Task GenerateInitData(string username, string password)
        {
            return TestService.GenerateInitData();
        }


        [HttpPost("page")]
        public Task GetTestPage(string username, string password)
        {
            return TestService.GetTestPage();
        }

        [HttpPost("update")]
        public Task updateTest()
        {
            return TestService.UpdateTest();
        }

        [HttpGet("elog/id={id}")]
        public Task<Elog> GetElogById(int id)
        {
            return TestService.QueryLog(id);
        }

        [HttpPost("elog/page={page}")]
        public Task<Tuple<int, IList<Elog>>> GetElogs(int page, int limit)
        {
            return TestService.QueryLog(page, limit);
        }

        [HttpPost("elog/add")]
        public Task InsertElogs(Elog elog)
        {
            return TestService.InsertLog(elog);
        }

        [HttpPost("redislock")]
        public Task RedisLockTest(Elog elog)
        {
            return TestService.RedisLockTest();
        }

        [HttpPost("redislock1")]
        public Task RedisLockTest1()
        {
            return TestService.RedisLockTest1();
        }

        [HttpPost("unifydata")]
        public Task<IdAndNameDto> GetData()
        {
            return TestService.GetData();
        }

        [HttpPost("unifydata/exception")]
        public Task<IdAndNameDto> GetDataException()
        {
            return TestService.GetDataException();
        }


        [HttpPost("unifydata/exception1")]
        public Task<IdAndNameDto> GetDataException1()
        {
            return TestService.GetDataException1();
        }

        [HttpPost("unifydata/exception2")]
        public IActionResult GetDataException2()
        {
            return PhysicalFile(@"C:\Users\narojay\Desktop\7.gif", "image/jpeg");
        }
    }
}