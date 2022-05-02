using Narojay.Blog.Models.Entity.Enum;

namespace Narojay.Blog.Models.Dto
{
    public class UserDto
    {
        public string UserName { get; set; }

        public Sex? Sex { get; set; }
        public string NickName { get; set; }

        public int Age { get; set; }

        public string Email { get; set; }

        public string Remarks { get; set; }
    }
}