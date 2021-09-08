using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("Comment")]
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserCommentId { get; set; }

    }
}
