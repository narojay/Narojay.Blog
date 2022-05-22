using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Tools.Core.Dto;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Blog.Aop;

namespace Narojay.Blog.Infrastructure.Service
{
    public class MessageBoardService : IMessageBoardService
    {
        public IMapper Mapper { get; set; }
        public BlogContext Context { get; set; }


        public async Task<LeaveMessage> AddLeaveMessageAsync(LeaveMessageDto message)
        {
            var masterRolePassword = await RedisHelper.GetAsync("masterRolePassword");
            User user;
            if (masterRolePassword == message.Email)
            {
                user = await Context.Users.FirstOrDefaultAsync(x => x.UserName == "narojay");
            }
            else
            {
                user = await Context.Users.FirstOrDefaultAsync(x => x.UserName == message.NickName);
                if (user is null)
                {
                    var newUser = new User
                    {
                        Email = message.Email,
                        NickName = message.NickName,
                        Age = new Random().Next(100),
                        UserName = message.NickName,
                        SampleRole = SampleRole.Visitor,
                    };
                    await Context.Users.AddAsync(newUser);
                    user = newUser;
                }
            }
            if (user == null)
            {
                throw new FriendlyException("咋回事呢？再试试呢？");
            }
            var leaveMessage = Mapper.Map<LeaveMessage>(message);
            leaveMessage.CreationTime = DateTime.Now;
            leaveMessage.Email = user.Email;
            leaveMessage.NickName = user.NickName;
            leaveMessage.IsMaster = user.SampleRole == SampleRole.Master;
            await Context.LeaveMessages.AddAsync(leaveMessage);
            var result = await Context.SaveChangesAsync();
            if (result <= 0) return leaveMessage;
            await RedisHelper.ZAddAsync($"leaveMessageReplySort:{message.ParentId}", (leaveMessage.CreationTime.Ticks, leaveMessage.Id));
            await RedisHelper.HSetAsync($"leaveMessageReplyContent:{message.ParentId}", leaveMessage.Id.ToString(), leaveMessage);

            return leaveMessage;
        }

        public async Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessagePageAsync(PageInputDto message)
        {
            var a = await RedisHelper.ZRevRangeAsync("leaveMessageContentSort", (message.PageIndex - 1) * message.PageSize, message.PageIndex * message.PageSize - 1);
            var leaveMessageDtos = new List<LeaveMessageDto>();

            foreach (var s in a)
            {
                var leaveMessageContent = await RedisHelper.HGetAsync("leaveMessageContent", s);

                var leaveMessage = JsonConvert.DeserializeObject<LeaveMessage>(leaveMessageContent);
                var leaveMessageDto = Mapper.Map<LeaveMessageDto>(leaveMessage);
                var replySort = await RedisHelper.ZRevRangeAsync($"leaveMessageReplySort:{leaveMessage.Id}", 0, 9);
                foreach (var item in replySort)
                {
                    var replyContent = await RedisHelper.HGetAsync($"leaveMessageReplyContent:{leaveMessage.Id}", item);
                    var leaveMessageReply = JsonConvert.DeserializeObject<LeaveMessage>(replyContent);
                    leaveMessageDto.Children.Add(Mapper.Map<LeaveMessageDto>(leaveMessageReply));
                }
                leaveMessageDtos.Add(leaveMessageDto);
            }

            return new PageOutputDto<LeaveMessageDto>
            {
                TotalCount = (int)await RedisHelper.ZCardAsync("leaveMessageContentSort"),
                Data = leaveMessageDtos
            };

        }


        public async Task<bool> RemoveLeaveMessageAsync(int id)
        {
            //Context.LeaveMessages.Remove(Context.LeaveMessages.Include(x => x.Children).First(x => x.Id == id));
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