using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;

namespace Narojay.Blog.Infrastructure.Service
{
    public class AdminAdminNoticeService : IAdminNoticeService
    {
        private readonly DataContext _context;

        public AdminAdminNoticeService(DataContext context)
        {
            _context = context;
        }


        public async Task<string> GetAdminNoticeAsync()
        {
            return await RedisHelper.CacheShellAsync(RedisPrefix.GetAdminNotice, 30 * 60 * 1000,
                async () =>
                {
                    var content = await _context.AdminNotices.Select(x => x.Content).FirstOrDefaultAsync();
                    return content;
                });
        }

        public async Task<bool> EditAdminNoticeAsync(string content)
        {
            await RedisHelper.DelAsync(RedisPrefix.GetAdminNotice);
            var model = await _context.AdminNotices.FirstOrDefaultAsync();
            if (model == null)
                await _context.AdminNotices.AddAsync(new AdminNotice
                {
                    Content = content
                });
            else
                model.Content = content;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}