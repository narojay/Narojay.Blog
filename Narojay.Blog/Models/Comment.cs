using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models
{
    [Table("Comment")]
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserCommentId { get; set; }

        [ForeignKey(nameof(UserCommentId))]
        public virtual BlogUser BlogUser { get; set; }
    }
}
