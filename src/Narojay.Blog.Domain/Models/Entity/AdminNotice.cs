using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Domain.Models.Entity;

[Table("adminnotice")]
public class AdminNotice : BaseEntity
{
    public string Content { get; set; }
}