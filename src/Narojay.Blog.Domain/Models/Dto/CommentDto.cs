namespace Narojay.Blog.Domain.Models.Dto;

public class CommentDto
{
    public string Content { get; set; }

    public int UserId { get; set; }

    public int PostId { get; set; }
}