using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Constant;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Extension;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;
using Narojay.Blog.Infrastruct.DataBase;

namespace Narojay.Blog.Application.Service;

public class SoliloquizeService : ISoliloquizeService
{
    private readonly BlogContext _blogContext;

    public SoliloquizeService(BlogContext blogContext)
    {
        _blogContext = blogContext;
    }

    public async Task<List<Soliloquize>> GetSoliloquizeListAsync(string content)
    {
        var soliloquizes =
            await RedisHelper.CacheShellAsync(RedisConstant.Soliloquzie, (int)TimeSpan.FromMinutes(2).TotalSeconds,
                async () =>
                {
                    var soliloquizes =
                        await _blogContext.Soliloquizes.AsNoTracking()
                            .WhereIf(string.IsNullOrEmpty(content), x => x.Content.Contains(content))
                            .OrderByDescending(x => x.CreationTime).ToListAsync();
                    return soliloquizes;
                });

        return soliloquizes;
    }

    public async Task<bool> AddSoliloquizeAsync(SoliloquizeRequest request)
    {
        var soliloquize = new Soliloquize(0, request.Content, DateTime.Now);
        await _blogContext.AddAsync(soliloquize);
        var result = await _blogContext.SaveChangesAsync() > 0;
        await RedisHelper.Instance.DelAsync(RedisConstant.Soliloquzie);
        return result;
    }

    public async Task<bool> ModifySoliloquizeAsync(SoliloquizeRequest request)
    {
        var soliloquize = await _blogContext.Soliloquizes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.Id);
        if (soliloquize is null) return true;
        _blogContext.Soliloquizes.Update(new Soliloquize(soliloquize.Id, request.Content, soliloquize.CreationTime));
        var result = await _blogContext.SaveChangesAsync() > 0;
        await RedisHelper.Instance.DelAsync(RedisConstant.Soliloquzie);
        return result;
    }

    public async Task<bool> RemoveSoliloquizeAsync(int id)
    {
        var soliloquize = await _blogContext.Soliloquizes.FindAsync(id);
        if (soliloquize is null) return true;
        _blogContext.Remove(soliloquize);
        var result = await _blogContext.SaveChangesAsync() > 0;
        await RedisHelper.Instance.DelAsync(RedisConstant.Soliloquzie);
        return result;
    }
}