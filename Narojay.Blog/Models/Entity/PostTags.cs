using Narojay.Blog.Models.Dto;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("post_tags")]
    public class PostTags : BaseEntity
    {

        protected PostTags()
        {

        }

        public PostTags(int postId, int tagId, Tag tagDto)
        {
            PostId = postId;
            TagId = tagId;
            Tag = tagDto == null ? null : new Tag(tagDto.Id, tagDto.Name);
        }
        public int PostId { get; set; }

        public int TagId { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
