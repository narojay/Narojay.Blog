using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;

namespace Narojay.Blog.Controllers;

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
    public Task<List<Soliloquize>> GetSoliloquizeAsync(string content)
    {
        return _soliloquizeService.GetSoliloquizeListAsync(content);
    }

    [HttpPost]
    public Task<bool> AddSoliloquizeAsync(SoliloquizeRequest request)
    {
        return _soliloquizeService.AddSoliloquizeAsync(request);
    }

    [HttpPut]
    public Task<bool> ModifySoliloquizeAsync(SoliloquizeRequest request)
    {
        return _soliloquizeService.ModifySoliloquizeAsync(request);
    }

    [HttpDelete]
    public Task<bool> RemoveSoliloquizeAsync(int id)
    {
        return _soliloquizeService.RemoveSoliloquizeAsync(id);
    }
}