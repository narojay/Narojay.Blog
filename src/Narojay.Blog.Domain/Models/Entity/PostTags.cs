using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Domain.Models.Entity;

[Table("post_tags")]
public class PostTags : BaseEntity
{
    protected PostTags()
    {
    }

    public PostTags(int postId, int tagId, string tagName = null)
    {
        if (!string.IsNullOrEmpty(tagName))
            Tag = new Tag(tagName);
        else if (tagId > 0) TagId = tagId;
        PostId = postId;
    }

    public int PostId { get; set; }

    public int TagId { get; set; }

    public virtual Tag Tag { get; set; }
}