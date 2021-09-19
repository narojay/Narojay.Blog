using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Configs
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {
            CreateMap<LeaveMessageDto, LeaveMessage>().ReverseMap();
            CreateMap<Post, PostDto>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<Comment, CommentDto>().ReverseMap();
        }
    }
}
