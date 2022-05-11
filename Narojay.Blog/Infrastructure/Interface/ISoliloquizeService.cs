using System.Collections.Generic;
using System.Threading.Tasks;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Infrastructure.Interface
{
    public interface ISoliloquizeService
    {
       Task<List<Soliloquize>> GetSoliloquizeListAsync(string content);

        Task<bool> AddSoliloquizeAsync(SoliloquizeRequest request);

        Task<bool> ModifySoliloquizeAsync(SoliloquizeRequest request);
        Task<bool> RemoveSoliloquizeAsync(int id);


    }
}
