using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("adminnotice")]
    public class AdminNotice : BaseEntity
    {
        public string Content { get; set; }
    }
}