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
        
        [MaxLength(150)]
        public string UserName { get; set; }

        public Sex?  Sex { get; set; }
        [MaxLength(150)]
        public string NickName { get; set; }
        
        public int Age { get; set; }

        public string Email { get; set; }

        public string Remarks { get; set; }


        public virtual  ICollection<Post> Posts { get; set; }
        public virtual  ICollection<LeaveMessage> LeaveMessages { get; set; }
    }
}
