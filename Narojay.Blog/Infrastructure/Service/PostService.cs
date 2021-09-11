using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.RedisModel;
using System.Threading.Tasks;

namespace Narojay.Blog.Infrastructure.Service
{
    public class PostService : IPostService
    {
        public DataContext Context { get; set; }

        public async Task<bool> AddPostAsync(Post post)
        {
            post.Content = Markdig.Markdown.ToHtml(post.Content);
            await Context.Posts.AddAsync(post);
            return await Context.SaveChangesAsync() > 0;
        }

        public async Task<Post> GetPostAsync(int id)
        {
            return await RedisHelper.CacheShellAsync(RedisPrefix.GetPost + id, 18000,
                        () => Context.Posts.FirstOrDefaultAsync(x => x.Id == id));
        }


    }
}
