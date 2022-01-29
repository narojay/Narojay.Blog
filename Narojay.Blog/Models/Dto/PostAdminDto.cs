using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Models.Dto
{
    public class PostAdminDto 
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime CreationTime { get; set; }

        public string Label { get; set; }
    }
}
