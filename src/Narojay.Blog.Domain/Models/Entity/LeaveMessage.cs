using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Domain.Models.Entity;

[Table("LeaveMessage")]
public class LeaveMessage : BaseEntity
{
    public LeaveMessage()
    {
        Children = new List<LeaveMessage>();
    }

    [MaxLength(500)] public string Content { get; set; }

    [MaxLength(100)] public string NickName { get; set; }

    [MaxLength(100)] public string Email { get; set; }

    public DateTime CreationTime { get; set; }

    public int ParentId { get; set; }

    [DefaultValue(false)] public bool IsMaster { get; set; }

    /// <summary>
    ///     父节点
    /// </summary>
    public virtual LeaveMessage Parent { get; set; }

    /// <summary>
    ///     子级
    /// </summary>
    public virtual ICollection<LeaveMessage> Children { get; set; }
}