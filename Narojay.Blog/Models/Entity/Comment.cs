using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("Comment")]
    public class Comment: BaseEntity
    {

        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

    }
}
