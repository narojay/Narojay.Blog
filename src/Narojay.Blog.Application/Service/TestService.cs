﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;
using Narojay.Blog.Domain.Models.Entity.Test;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Application.Service;

public class TestService : ITestService
{
    private static readonly char[] constant =
    {
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u',
        'v', 'w', 'x', 'y', 'z',
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
        'V', 'W', 'X', 'Y', 'Z'
    };

    private readonly BlogContext _dbContext;

    public TestService(BlogContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task UpdateTest()
    {
        var f = TestEnum.A | TestEnum.B | TestEnum.C;


        var d = await _dbContext.TestAccounts.Where(x => (x.TestEnum & TestEnum.C) == TestEnum.C).ToListAsync();
        //if (d.Value.HasFlag(TestEnum.A))
        //{

        //}
        var a = new TestAccount
        {
            Id = 160912,
            UserId = 6
        };
        _dbContext.Entry(a).State = EntityState.Unchanged;

        _dbContext.Entry(a).Property(x => x.UserId).IsModified = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task InsertLog(Elog log)
    {
        //var client = _elasticsearchService.GetClient();
        //var a = await _elasticsearchService.GetClient().IndexAsync(log, x => x.Index("test"));
    }

    public Task<Tuple<int, IList<Elog>>> QueryLog(int page, int limit)
    {
        throw new NotImplementedException();
    }

    public Task<Elog> QueryLog(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RedisLockTest()
    {
        if (await RedisHelper.Instance.SetNxAsync("lock_test_1", 1))
        {
            Console.WriteLine("test redis lock ");
            await RedisHelper.Instance.ExpireAsync("lock_test_1", 2);
            return true;
        }

        Console.WriteLine("task is lock");
        return false;
    }

    public async Task RedisLockTest1()
    {
        /*var a = await _dbContext.Users.Where(x => x.Id == 2).Select(x => new IdAndNameDto
        {
            Id = x.TestAccount == null ? -1 : x.TestAccount.Price
        }).ToListAsync();*/
    }

    public Task<IdAndNameDto> GetData()
    {
        //throw new Exception("test");
        return Task.FromResult<IdAndNameDto>(null);
    }

    public Task<IdAndNameDto> GetDataException()
    {
        throw new StringResponseException("统一异常处理异常测试");
    }

    public Task<IdAndNameDto> GetDataException1()
    {
        var a = 1;
        var b = 0;
        var c = a / b;
        //throw new Exception("test");
        return Task.FromResult<IdAndNameDto>(null);
        ;
    }
    //public async Task GenerateInitData()
    //{
    //    var random = new Random();
    //    var testUsers = new List<TestUser>();
    //    for (int i = 0; i < 300000; i++)
    //    {
    //        var testAccountList = new List<TestAccount>();
    //        for (int j = 0; j < 5; j++)
    //        {
    //            testAccountList.Add(new TestAccount
    //            {
    //                AccountName = GenerateRandomNumber(10),
    //                AccountNo = random.Next(1000000000).ToString(),
    //            });
    //        }
    //        var testUser = new TestUser
    //        {
    //            Name = GenerateRandomNumber(10),
    //            Age = (byte)random.Next(100),
    //            Email = GenerateRandomNumber(10) + "@126.com",
    //            Phone = "1" + random.Next(1000000000),
    //            TestAccount = testAccountList
    //        };
    //        testUsers.Add(testUser);
    //    }
    //    await _dbContext.BulkInsertAsync(testUsers);

    //    var testAccounts = new List<TestAccount>();
    //    foreach (var item in testUsers)
    //    {
    //        item.TestAccount.ForEach(x => x.UserId = item.Id);
    //        testAccounts.AddRange(item.TestAccount);
    //    }
    //    await _dbContext.BulkInsertAsync(testAccounts);
    //}

    public Task GenerateInitData()
    {
        throw new NotImplementedException();
    }

    public async Task GetTestPage()
    {
        var stop = new Stopwatch();
        stop.Start();
        var aaa = await _dbContext.TestUsers.AsNoTracking().Where(x => x.Age == 95).Select(x => new
        {
            x.Id,
            Name = x.Email
        }).ToListAsync();
        var a = await _dbContext.TestUsers.AsNoTracking().Where(x => aaa.Select(y => y.Id).Contains(x.Id))
            .ToListAsync();
        stop.Stop();
        Console.WriteLine(stop.ElapsedMilliseconds);
        await _dbContext.TestUsers.AsNoTracking().Where(x => x.Id == 106052).GroupJoin(_dbContext.TestAccounts,
            g => g.Id,
            t => t.UserId,
            (g, t) => new
            {
                g.Age,
                t
            }).SelectMany(x => x.t.DefaultIfEmpty(), (x, y) => new
        {
            x.Age,
            UserId = y.UserId > 0 ? y.UserId : -1,
            Id = y.Id > 0 ? y.Id : -1
        }).ToListAsync();
    }

    public Task UpdateLog(Elog elog)
    {
        throw new NotImplementedException();
    }

    //public async Task<Tuple<int, IList<Elog>>> QueryLog(int page, int limit)
    //{
    //    var query = await _elasticsearchService.GetClient().SearchAsync<Elog>(x => x.Index("test")
    //        .From((page - 1) * limit)
    //        .Size(limit)
    //    );
    //    return new Tuple<int, IList<Elog>>(Convert.ToInt32(query.Total), query.Documents.ToList());
    //}

    //public async Task<Elog> QueryLog(int id)
    //{
    //    var model = await _elasticsearchService.GetClient().GetAsync<Elog>(id, x => x.Index("test"));
    //    return model.Source;
    //}

    public Task DeleteLog(int id)
    {
        throw new NotImplementedException();
    }


    public static string GenerateRandomNumber(int Length)
    {
        var newRandom = new StringBuilder(62);
        var rd = new Random();
        for (var i = 0; i < Length; i++) newRandom.Append(constant[rd.Next(62)]);
        return newRandom.ToString();
    }
}