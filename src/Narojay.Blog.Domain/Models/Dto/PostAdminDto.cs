using System;

namespace Narojay.Blog.Domain.Models.Dto;

public class PostAdminDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public DateTime CreationTime { get; set; }

    public string Label { get; set; }
}