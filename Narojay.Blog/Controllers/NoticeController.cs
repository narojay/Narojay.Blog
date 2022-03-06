using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;

namespace Narojay.Blog.Controllers
{
    [Route("notice")]
    public class NoticeController : BaseController
    {
        private readonly IAdminNoticeService _adminNoticeService;

        public NoticeController(IAdminNoticeService adminNoticeService)
        {
            _adminNoticeService = adminNoticeService;
        }


        [HttpGet("message")]
        public Task<string> GetAdminNotice() => _adminNoticeService.GetAdminNoticeAsync();

        [HttpPost("msessage/edit")]
        public Task<bool> EditAdminNoticeAsync(string content) => _adminNoticeService.EditAdminNoticeAsync(content);
    }
}
