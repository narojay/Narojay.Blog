using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Narojay.Blog.Models.Dto
{
    public class PostTagDto
    {
        [Required]
        public int PostId { get; set; }


        public List<int> TagIds { get; set; } = new();

        public List<string> TagNames { get; set; } = new();

    }
}
