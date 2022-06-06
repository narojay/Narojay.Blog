using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;

namespace Narojay.Blog.Application.Interface;

public interface ISoliloquizeService
{
    Task<List<Soliloquize>> GetSoliloquizeListAsync(string content);

    Task<bool> AddSoliloquizeAsync(SoliloquizeRequest request);

    Task<bool> ModifySoliloquizeAsync(SoliloquizeRequest request);
    Task<bool> RemoveSoliloquizeAsync(int id);
}