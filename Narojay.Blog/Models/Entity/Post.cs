using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("Post")]
    public class Post :BaseEntity
    {

        public Post()
        {
            Comments = new HashSet<Comment>();
        }
        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ModifyTime { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
