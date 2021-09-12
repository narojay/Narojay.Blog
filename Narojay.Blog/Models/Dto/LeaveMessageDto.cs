using System;

namespace Narojay.Blog.Models.Dto
{
    public class LeaveMessageDto
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
