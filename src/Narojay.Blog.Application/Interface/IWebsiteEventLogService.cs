using System.Threading.Tasks;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Application.Interface;

public interface IWebsiteEventLogService
{
    Task<PageOutputDto<WebsiteEventLogOutputDto>> GetPagingWebsiteEventLogAsync(WebsiteEventLogQuery pageInputDto);
    Task<bool> RemoveWebsiteEventLogAsync(int id);
    Task<bool> UpdateWebsiteEventLogAsync(WebsiteEventLogDto websiteEventLogDto);
    Task<bool> AddWebsiteEventLogAsync(WebsiteEventLogDto websiteEventLogDto);
}