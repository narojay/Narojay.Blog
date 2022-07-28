using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Domain.Models.Dto;

public class PostAdminDtoRequest : PageInputDto
{
    public string Title { get; set; }
    public string Label { get; set; }
}