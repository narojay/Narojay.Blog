using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Domain.Models.Entity;

[Table("website_event_log")]
public class WebsiteEventLog
{
    public int Id { get; set; }

    public string Content { get; set; }

    public DateTime CreationTime { get; set; }

    public bool IsDeleted { get; set; }


    public DateTime? LastModifyTime { get; set; }
}