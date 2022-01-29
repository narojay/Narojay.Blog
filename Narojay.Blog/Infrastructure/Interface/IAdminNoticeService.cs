using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface IAdminNoticeService
    {


        Task<string> GetAdminNoticeAsync();
        Task<bool> EditAdminNoticeAsync(string content);
    }
}
