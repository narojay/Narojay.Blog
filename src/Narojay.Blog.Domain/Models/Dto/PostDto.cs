using System;
using System.Collections.Generic;

namespace Narojay.Blog.Domain.Models.Dto;

public class PostDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Content { get; set; }

    public string Author { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public bool IsTop { get; set; }

    public string Label { get; set; }

    public int LikeCount { get; set; }

    public int UnlikeCount { get; set; }

    public int UserId { get; set; }

    public UserDto User { get; set; }

    public List<CommentDto> CommentDtos { get; set; }

    public PostTagDto PostTagDto { get; set; }
}