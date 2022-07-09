using Microsoft.AspNetCore;
using Moq;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Application.Service;
using Narojay.Blog.Domain.Models.Dto;

namespace Narojay.Blog.Test;

public class UnitTest1
{
  
    [Fact]
    public async Task Test1()
    {
        int id = 32;
        var customerRepositoryMock = new Mock<IPostService>();
        var postDto = new PostDto();
        customerRepositoryMock.Setup(x => x.GetPostByIdAsync(id))
            .ReturnsAsync( postDto);
        
        var sut = new PostService(null, null);
      var result =  await  sut.GetPostByIdAsync(id);
    }
}