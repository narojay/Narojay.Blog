using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Narojay.Blog.Models.Entity
{
    [Table("Post")]
    public class Post : BaseEntity
    {
        [MaxLength(50)] public string Title { get; set; }

        [MaxLength(8000)] public string Content { get; set; }

        [MaxLength(50)] public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModifyTime { get; set; }

        [DefaultValue(0)] public int LikeCount { get; set; }

        [DefaultValue(0)] public int UnlikeCount { get; set; }

        public int UserId { get; set; }

        [MaxLength(255)] public string Label { get; set; }

        [DefaultValue(false)] public bool IsTop { get; set; }
        [JsonIgnore]
        public virtual User User { get; set; }
        [JsonIgnore]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}