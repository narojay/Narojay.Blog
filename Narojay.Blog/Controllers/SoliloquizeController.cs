using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Infrastructure.Interface;
using Narojay.Blog.Models.Dto;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Controllers
{
    /// <summary>
    ///     文章
    /// </summary>
    [Route("soliloquize")]
    public class SoliloquizeController : BaseController
    {
        private readonly ISoliloquizeService _soliloquizeService;

        public SoliloquizeController(ISoliloquizeService soliloquizeService)
        {
            _soliloquizeService = soliloquizeService;
        }

        [HttpGet]
        public Task<List<Soliloquize>> GetSoliloquizeAsync(string content) =>
            _soliloquizeService.GetSoliloquizeListAsync(content);

        [HttpPost]
        public Task<bool> AddSoliloquizeAsync(SoliloquizeRequest request) =>
            _soliloquizeService.AddSoliloquizeAsync(request);

        [HttpPut]
        public Task<bool> ModifySoliloquizeAsync(SoliloquizeRequest request) =>
            _soliloquizeService.ModifySoliloquizeAsync(request);

        [HttpDelete]
        public Task<bool> RemoveSoliloquizeAsync(int id) =>
            _soliloquizeService.RemoveSoliloquizeAsync(id);
    }
}
