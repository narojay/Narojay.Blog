using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Models.RedisModel
{
    public class RedisPrefix
    {
        public const string GetAllUser = "GetAllUser";
        public const string GetPost = "GetPost";
        public const string GetLeaveMessagePageAsync = "GetLeaveMessagePageAsync";
        public const string GetLeaveMessagePageCountAsync = "GetLeaveMessagePageCountAsync";
        public const string GetPostListAsync = "GetPostListAsync";
    }
}
