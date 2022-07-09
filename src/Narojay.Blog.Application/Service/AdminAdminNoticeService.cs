﻿using System.Data;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Entity;
using Narojay.Blog.Domain.Models.RedisModel;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Application.Service;

public class AdminAdminNoticeService : IAdminNoticeService
{
    private readonly BlogContext _context;

    public AdminAdminNoticeService(BlogContext context)
    {
        _context = context;
    }


    public async Task<string> GetAdminNoticeAsync()
    {
        //using (var channel = _connection.CreateModel())
        //{
        //    channel.ConfirmSelect();
        //    channel.ExchangeDeclare(exchange: "narojay_blog_exchange", type: "direct", false, false, null);
        //    channel.QueueDeclare("test_queue", false, false, false, null);
        //    channel.QueueBind("test_queue", "narojay_blog_exchange", "", null);
        //    for (int i = 0; i < 10; i++)
        //    {
        //        channel.BasicPublish("narojay_blog_exchange", "", true, null, Encoding.UTF8.GetBytes("test"));

        //    }
        //}

        //return await RedisHelper.CacheShellAsync(RedisPrefix.GetAdminNotice, 30 * 60 * 1000,
        //    async () =>
        //    {
        //        var content = await _context.AdminNotices.Select(x => x.Content).FirstOrDefaultAsync();
        //        return content;
        //    });
        return "";
    }

    public async Task<bool> EditAdminNoticeAsync(string content)
    {
        await RedisHelper.DelAsync(RedisPrefix.GetAdminNotice);
        var model = await _context.AdminNotices.FirstOrDefaultAsync();
        if (model == null)
            await _context.AdminNotices.AddAsync(new AdminNotice
            {
                Content = content
            });
        else
            model.Content = content;

        return await _context.SaveChangesAsync() > 0;
    }

    public async Task TestTranscation()
    {
        using (var aaa = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted))
        {
            try
            {
                await _context.AdminNotices.AddAsync(new AdminNotice
                {
                    Content = "123"
                });


                await _context.SaveChangesAsync();

                await _context.AdminNotices.AddAsync(new AdminNotice
                {
                    Content = "1234"
                });
                await _context.SaveChangesAsync();
                //throw new Exception("test"); 
                await aaa.CommitAsync();
            }
            catch (Exception e)
            {
                await aaa.RollbackAsync();
            }
        }
    }
}