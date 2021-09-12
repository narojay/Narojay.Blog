using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using Narojay.Tools.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class MessageBoardService : IMessageBoardService
    {
        public IMapper Mapper { get; set; }
        public DataContext Context { get; set; }


        public async Task<LeaveMessage> AddLeaveMessageAsync(LeaveMessageDto message)
        {
            var leaveMessage = Mapper.Map<LeaveMessage>(message);
            leaveMessage.CreationTime = DateTime.Now; ;
            await Context.LeaveMessages.AddAsync(leaveMessage);
            await Context.SaveChangesAsync();
            return leaveMessage;

        }

        public async Task<PageOutputDto<LeaveMessageDto>> GetLeaveMessagePageAsync(PageInputDto message)
        {
            var list = await RedisHelper.CacheShellAsync(RedisPrefix.GetLeaveMessagePageAsync + message.PageIndex, 1000, async () =>
                       {
                           var model = await Context.LeaveMessages.OrderByDescending(x => x.CreationTime)
                               .Skip((message.PageIndex - 1) * message.PageSize).Take(message.PageSize)
                               .ToListAsync();
                           var leaveMessageDtos = Mapper.Map<List<LeaveMessageDto>>(model);
                           return leaveMessageDtos;

                       });
            var totalCount = await RedisHelper.CacheShellAsync(RedisPrefix.GetLeaveMessagePageCountAsync, 1000,
                () => Context.LeaveMessages.CountAsync());

            return new PageOutputDto<LeaveMessageDto>
            {
                Data = list,
                TotalCount = totalCount
            };

        }
    }
}
