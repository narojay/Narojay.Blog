using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Models.Dto
{
    public class PostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string Author { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModifyTime { get; set; }

        public int UserId { get; set; }

        public  UserDto User { get; set; }

        public  ICollection<CommentDto> Comments { get; set; }
    }
}
