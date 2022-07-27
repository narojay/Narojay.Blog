using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Application.Events;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;
using Narojay.Blog.Infrastruct.DataBase;
using Narojay.Blog.Infrastruct.Elasticsearch;
using Nest;

namespace Narojay.Blog.Controllers;

[Route("test")]
public class TestController : BaseController
{
    private readonly BlogContext _blogContext;
    private readonly IElasticsearchProvider _elasticsearchProvider;
    private readonly IEventBus _eventBus;
    private readonly ILogger<TestController> _logger;

    public TestController(BlogContext blogContext, IElasticsearchProvider elasticsearchProvider, IEventBus eventBus,
        ILogger<TestController> logger)
    {
        _blogContext = blogContext;
        _elasticsearchProvider = elasticsearchProvider;
        _eventBus = eventBus;
        _logger = logger;
    }

    public ITestService TestService { get; set; }

    [HttpPost("es")]
    public async Task Test1(string content)
    {
        var posts = await _blogContext.Posts.AsNoTracking().ToListAsync();
        var elasticClient = _elasticsearchProvider.GetClient();
        // var a11 = new { Name = "test" };
        // var ac1 =  await elasticClient.IndexAsync(testAccount,x => x.Index("test_account"));
        // var ac2 =  await elasticClient.IndexAsync(a11,x => x.Index("test1"));
        // var ac3 =  await elasticClient.IndexAsync(a11,x => x.Index("test2").Id("12"));
        // var a =   elasticClient.Search<TestAccount>(s => s.Index("test_account").From(0).Size(10));
        var a = await elasticClient.IndexManyAsync(posts, "post");

        var result = await elasticClient.SearchAsync<Post>(s =>
            s.Index("post").Query(x =>
                    x.Match(m =>
                        m.Field(f => f.Content).Query(content)))
                .Highlight(h => h.PreTags("<em>")
                    .PostTags("</em>")
                    .Fields(x => x.Field(f => f.Content)
                        .FragmentSize(10)
                        .NumberOfFragments(4))));
    }

    [HttpPost("test")]
    public void Test(CreateOrderEvent createOrderEvent)
    {
        _eventBus.Publish(createOrderEvent);
    }
    //[HttpPost("test")]
    //public async Task GenerateInitData(string username, string password)
    //{

    //var posts = await BlogContext.Posts.AsNoTracking().ToListAsync();
    //foreach (var post in posts)
    //{
    //    await RedisHelper.ZAddAsync($"PostSortByTime", (post.CreationTime.Ticks, post.Id));
    //    await RedisHelper.HSetAsync("PostContent", post.Id.ToString(), post);
    //}

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
    //var a = await BlogContext.LeaveMessages.AsNoTracking().Where(x => x.ParentId > 0).ToListAsync();
    //var groupBy = a.GroupBy(x => x.ParentId);
    //foreach (var item in groupBy)
    //{
    //    foreach (var item1 in item)
    //    {
    //        await RedisHelper.ZAddAsync($"leaveMessageReplySort:{item1.ParentId}", (item1.CreationTime.Ticks, item1.Id));
    //        await RedisHelper.HSetAsync($"leaveMessageReplyContent:{item1.ParentId}", item1.Id.ToString(),
    //            item1);
    //    }
    //}
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
    //}


    [HttpPost("page")]
    public async Task<bool> GetTestPage(string username, string password)
    {
        //try
        //{


        //    var stopwatch = new Stopwatch();
        //    stopwatch.Start();

        //    var list = Enumerable.Range(0, 10).Select(x =>
        //    {
        //        var tag = new Tag(0, x.ToString());
        //        return new PostTags
        //        {
        //            PostId = 32,
        //            TagId = tag.Id,
        //            Tag = tag
        //        };
        //    });

        //    using var connection = new MySqlConnection(AppConfig.ConnString);
        //    var options = new DbContextOptionsBuilder<BlogContext>()
        //        .UseMySql(AppConfig.ConnString, ServerVersion.AutoDetect(AppConfig.ConnString)).Options;
        //    using var context1 = new BlogContext(options);
        //    using var c = BlogContext.Database.BeginTransaction();
        //    await BlogContext.AddRangeAsync(list);
        //    await BlogContext.SaveChangesAsync();
        //    using var context2= new BlogContext(options);
        //    Console.WriteLine();


        //var blog = Factory.CreateDbContext();

        //var post = await blog.PostTags.FirstOrDefaultAsync();
        //EntityFrameworkManager.ContextFactory = context => Factory.CreateDbContext();
        //await using var context = await Factory.CreateDbContextAsync();
        //var transation = context.Database.BeginTransaction();
        //var tasks = new List<Task>();
        //using var context1 =  Factory.CreateDbContext();
        //context1.Database.UseTransaction(transation.GetDbTransaction());
        //context.BulkInsert(list, x => x.IncludeGraph = true);
        //await transation.CommitAsync();
        //Console.WriteLine("1 :" + stopwatch.ElapsedMilliseconds);

        //stopwatch.Restart();


        return true;
        //}
        //catch (Exception e)
        //{
        //    Console.WriteLine(e);
        //    throw;
        //}
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