using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Narojay.Blog.Domain.Models.Entity;

[Table("Post")]
public class Post : BaseEntity
{
    public Post(string title, string content, string author, bool isTop, int userId)
    {
        Title = title;
        Content = content;
        Author = author;
        IsTop = isTop;
        CreationTime = DateTime.Now;
        LikeCount = 0;
        UnlikeCount = 0;
        UserId = userId;
        PostTags = new List<PostTags>();
        Comments = new List<Comment>();
    }

    [MaxLength(50)] public string Title { get; set; }

    [MaxLength(8000)] public string Content { get; set; }

    [MaxLength(50)] public string Author { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? ModifyTime { get; set; }

    [DefaultValue(0)] public int LikeCount { get; set; }

    [DefaultValue(0)] public int UnlikeCount { get; set; }

    public int UserId { get; set; }

    [MaxLength(255)] public string Label { get; set; }

    [DefaultValue(false)] public bool IsTop { get; set; }

    [JsonIgnore] public virtual User User { get; set; }

    [JsonIgnore] public virtual ICollection<Comment> Comments { get; set; }


    public virtual ICollection<PostTags> PostTags { get; set; }


    public void AddPostTags(int tagId, string tagName = null)
    {
        var postTag = new PostTags(Id, tagId, tagName);
        PostTags.Add(postTag);
    }
}