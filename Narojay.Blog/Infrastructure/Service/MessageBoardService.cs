using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;
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
            Context.LeaveMessages.Remove(Context.LeaveMessages.Include(x =>x.Children).First(x => x.Id == id));
            return await Context.SaveChangesAsync() > 0;
        }
    }
}
