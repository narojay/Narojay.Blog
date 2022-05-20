using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Controllers
{
    [Route("test")]
    public class TestController : BaseController
    {
        public ITestService TestService { get; set; }

        public BlogContext BlogContext { get; set; }

        [HttpPost("test")]
        public async Task GenerateInitData(string username, string password)
        {

            var posts =  await BlogContext.Posts.AsNoTracking().ToListAsync();
            foreach (var post in posts)
            {        
                   await RedisHelper.ZAddAsync($"PostSortByTime", (post.CreationTime.Ticks,post.Id));
                  await  RedisHelper.HSetAsync("PostContent",post.Id.ToString(),post);
            }

            //var leaveMessages = await BlogContext.LeaveMessages.AsNoTracking().Where(x => x.ParentId ==0 ).ToListAsync();
            ////foreach (var item in leaveMessages)
            ////{
            ////    await RedisHelper.ZAddAsync($"leaveMessageOrder", (item.CreationTime.Ticks, item.Id));

            ////}
            //foreach (var item in leaveMessages)
            //{
            //    await RedisHelper.ZAddAsync($"leaveMessageContentSort", (item.CreationTime.Ticks, item.Id));
            //    RedisHelper.HSet("leaveMessageContent", item.Id.ToString(), item);
            //}

            //var test = await BlogContext.LeaveMessages.AsNoTracking().Where(x => x.ParentId > 0).GroupBy(x => x.ParentId).ToListAsync();
            var a = await BlogContext.LeaveMessages.AsNoTracking().Where(x => x.ParentId > 0).ToListAsync();
            var groupBy = a.GroupBy(x => x.ParentId);
            foreach (var item in groupBy)
            {
                foreach (var item1 in item)
                {
                    await RedisHelper.ZAddAsync($"leaveMessageReplySort:{item1.ParentId}", (item1.CreationTime.Ticks, item1.Id));
                    await RedisHelper.HSetAsync($"leaveMessageReplyContent:{item1.ParentId}", item1.Id.ToString(),
                        item1);
                }
            }
            //var cc = new Dictionary<string, string>
            //{
            //    { "F1", "string" },
            //    { "F2", "true" }
            //};
            //var ccd = new Dictionary<string, string>
            //{
            //    { "F1", "string1" },
            //    { "F2", "true2" }
            //};
            //var keyValues1 = cc.Select(a => new[] { a.Key, a.Value.ToString() }).SelectMany(a => a).ToArray();
            //var keyValues2 = ccd.Select(a => new[] { a.Key, a.Value.ToString() }).SelectMany(a => a).ToArray();
            //await RedisHelper.HMSetAsync("leaveMessageContent", keyValues1);
            //await RedisHelper.HMSetAsync("leaveMessageContent", keyValues2);
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