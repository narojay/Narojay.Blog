using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Narojay.Blog.Models.Entity
{
    [Table("Soliloquize")]
    public class Soliloquize : BaseEntity
    {
        protected Soliloquize()
        {

        }
        public Soliloquize(int id, string content, DateTime dateTime)
        {
            Content = content;
            Id = id;
            CreationTime = dateTime;
        }
     
        [MaxLength(500)]
        public string Content { get;  set; }

        public DateTime CreationTime { get;  set; }

    }
}
