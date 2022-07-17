using System;
using System.Collections.Generic;

namespace Narojay.Blog.Domain.Models.Dto;

public class LeaveMessageDto
{
    public int Id { get; set; }

    public string Content { get; set; }

    public string NickName { get; set; }

    public string Email { get; set; }

    public int ParentId { get; set; }

    public bool IsMaster { get; set; }

    public DateTime CreationTime { get; set; }

    public ICollection<LeaveMessageDto> Children { get; set; }
}