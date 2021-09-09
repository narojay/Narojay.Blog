﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Narojay.Blog.Models.Enum;

namespace Narojay.Blog.Models.Entity
{
    [Table("User")]
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public Sex?  Sex { get; set; }

        public string NickName { get; set; }
        
        public int Age { get; set; }

        public string Email { get; set; }

        public string Remarks { get; set; }


        public virtual  ICollection<Post> Posts { get; set; }
    }
}
