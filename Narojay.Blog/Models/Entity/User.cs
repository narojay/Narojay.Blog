using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Narojay.Blog.Models.Entity.Enum;

namespace Narojay.Blog.Models.Entity
{
    [Table("User")]
    public class User : BaseEntity
    {
        
        [MaxLength(20)]
        public string UserName { get; set; }

        public Sex?  Sex { get; set; }
        [MaxLength(20)]
        public string NickName { get; set; }
        
        public int Age { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(500)]
        public string Remarks { get; set; }
        [MaxLength(200)]
        public string Password { get; set; }

        public virtual  ICollection<Post> Posts { get; set; }
        public virtual  ICollection<LeaveMessage> LeaveMessages { get; set; }
    }
}
