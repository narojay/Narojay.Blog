using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Models.Dto
{
    public class CommentDto
    {
        public string Content { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }

    }
}
