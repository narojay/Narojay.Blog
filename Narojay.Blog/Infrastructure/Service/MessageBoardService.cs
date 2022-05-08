using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Infrastructure.Service
{
    public class MessageBoardService : IMessageBoardService
    {
        public IMapper Mapper { get; set; }
        public DataContext Context { get; set; }


        public async Task<LeaveMessage> AddLeaveMessageAsync(LeaveMessageDto message)
        {
            var leaveMessage = Mapper.Map<LeaveMessage>(message);
            leaveMessage.CreationTime = DateTime.Now;
            ;
            await Context.LeaveMessages.AddAsync(leaveMessage);
            leaveMessage.IsMaster = leaveMessage.Email == "hj200812@126.com";
            await Context.SaveChangesAsync();
            return leaveMessage;
        }

        public async Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessagePageAsync(PageInputDto message)
        {
            var query = Context.LeaveMessages.Where(x => x.ParentId == 0);
            var model = await query.OrderByDescending(x => x.CreationTime)
                .Skip((message.PageIndex - 1) * message.PageSize).Take(message.PageSize)
                .ToListAsync();
            var leaveMessageDtos = Mapper.Map<List<LeaveMessageDto>>(model);

            var totalCount = await query.CountAsync();

            return new PageOutputDto<LeaveMessageDto>
            {
                Data = leaveMessageDtos,
                TotalCount = totalCount
            };
        }

        public async Task<bool> RemoveLeaveMessageAsync(int id)
        {
            Context.LeaveMessages.Remove(Context.LeaveMessages.Include(x => x.Children).First(x => x.Id == id));
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<bool> BatchLeaveMessageAsync(int num)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var leaveMessages = new List<LeaveMessage>();

            for (var a = 0; a < num; a++)
            {
                var leaveMessage = new LeaveMessage
                {
                    Content = new Random().Next(1000000).ToString(),
                    CreationTime = DateTime.Now,
                    Email = new Random().Next(1000000) + new Random().Next(1000000).ToString(),
                    IsMaster = false,
                    NickName = "test1"
                };
                leaveMessages.Add(leaveMessage);
            }

            await Context.LeaveMessages.AddRangeAsync(leaveMessages);
            await Context.SaveChangesAsync();
            stopwatch.Stop();
            Console.WriteLine("1------" + stopwatch.ElapsedMilliseconds);
            stopwatch.Restart();
            for (var a = 0; a < num; a++)
            {
                var leaveMessage = new LeaveMessage
                {
                    Content = new Random().Next(1000000).ToString(),
                    CreationTime = DateTime.Now,
                    Email = new Random().Next(1000000) + new Random().Next(1000000).ToString(),
                    IsMaster = false,
                    NickName = "test1"
                };
                Context.LeaveMessages.Add(leaveMessage);
            }

            await Context.SaveChangesAsync();
            stopwatch.Stop();
            Console.WriteLine("2------" + stopwatch.ElapsedMilliseconds);
            return true;
        }

        public async Task<bool> BatchUpdateLeaveMessageAsync(int num)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var listAsync = await Context.LeaveMessages.Take(num).ToListAsync();
            listAsync.ForEach(x => x.Content = new Random().Next(10000).ToString());
            var a = await Context.SaveChangesAsync() > 0;
            stopwatch.Stop();
            Console.WriteLine("3------" + stopwatch.ElapsedMilliseconds);
            return a;
        }
    }
}