using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("LeaveMessage")]
    public class LeaveMessage : BaseEntity
    {
        [MaxLength(500)]
        public string Content { get; set; }

        public int UserId { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual User User { get; set; }
    }
}
