using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Controllers;

/// <summary>
///     建站日志
/// </summary>
[Route("website_event_log")]
public class WebSiteEventLogController : BaseController
{
    private readonly IWebsiteEventLogService _websiteEventLogService;

    public WebSiteEventLogController(IWebsiteEventLogService websiteEventLogService)
    {
        _websiteEventLogService = websiteEventLogService;
    }

    /// <summary>
    ///     建站日志分页
    /// </summary>
    /// <param name="websiteEventLogQuery"></param>
    /// <returns></returns>
    [HttpGet("list")]
    public Task<PageOutputDto<WebsiteEventLogOutputDto>> GetPagingWebsiteEventLogAsync(
        [FromQuery] WebsiteEventLogQuery websiteEventLogQuery)
    {
        return _websiteEventLogService.GetPagingWebsiteEventLogAsync(websiteEventLogQuery);
    }

    /// <summary>
    ///     添加建站日志
    /// </summary>
    /// <param name="websiteEventLogDto"></param>
    /// <returns></returns>
    [HttpPost("add")]
    public Task<bool> AddWebsiteEventLogAsync(WebsiteEventLogDto websiteEventLogDto)
    {
        return _websiteEventLogService.AddWebsiteEventLogAsync(websiteEventLogDto);
    }


    /// <summary>
    ///     删除建站日志
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost("remove")]
    public Task<bool> RemoveWebsiteEventLogAsync(int id)
    {
        return _websiteEventLogService.RemoveWebsiteEventLogAsync(id);
    }

    /// <summary>
    ///     修改建站日志
    /// </summary>
    /// <param name="websiteEventLogDto"></param>
    /// <returns></returns>
    [HttpPost("update")]
    public Task<bool> UpdateWebsiteEventLogAsync(WebsiteEventLogDto websiteEventLogDto)
    {
        return _websiteEventLogService.UpdateWebsiteEventLogAsync(websiteEventLogDto);
    }
}