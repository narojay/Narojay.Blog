using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("Post")]
    public class Post :BaseEntity
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public int UserId { get; set; }

        public virtual  User User { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
