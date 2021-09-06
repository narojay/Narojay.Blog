using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models
{
    [Table("BlogUser")]
    public class BlogUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public string Remarks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}
