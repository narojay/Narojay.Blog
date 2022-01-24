using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Models.Dto
{
    public class PostAdminDtoRequest :PageInputDto
    {

        public string Title { get; set; }

        public string Label { get; set; }
    }
}
