using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Configs
{
    public class AppConfig
    {
        public static string Redis { get; set; }


        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string ConnString { get; set; }
    }
}
