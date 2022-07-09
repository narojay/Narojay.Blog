using AutoMapper;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;

namespace Narojay.Blog.Application.AutoMapper;

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