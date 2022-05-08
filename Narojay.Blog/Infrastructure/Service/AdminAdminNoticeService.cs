using EventBusRabbitMQ;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using System;
using System.Text;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity.Test;
using IsolationLevel = System.Data.IsolationLevel;

namespace Narojay.Blog.Infrastructure.Service
{
    public class AdminAdminNoticeService : IAdminNoticeService
    {
        private readonly DataContext _context;
        private readonly IRabbitMQPersistentConnection _connection;

        public AdminAdminNoticeService(DataContext context, IRabbitMQPersistentConnection connection)
        {
            _context = context;
            _connection = connection;
        }


        public async Task<string> GetAdminNoticeAsync()
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ConfirmSelect();
                channel.ExchangeDeclare(exchange: "narojay_blog_exchange", type: "direct", false, false, null);
                channel.QueueDeclare("test_queue", false, false, false, null);
                channel.QueueBind("test_queue", "narojay_blog_exchange", "", null);
                for (int i = 0; i < 10; i++)
                {
                    channel.BasicPublish("narojay_blog_exchange", "", true, null, Encoding.UTF8.GetBytes("test"));

                }
            }

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
                        Content = "123",
                    });


                    await _context.SaveChangesAsync();

                    await _context.AdminNotices.AddAsync(new AdminNotice
                    {
                        Content = "1234",
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
}