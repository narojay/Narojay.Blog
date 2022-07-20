using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Application.Interface;
using Narojay.Blog.Domain;
using Narojay.Blog.Domain.Extension;
using Narojay.Blog.Domain.Models.Dto;
using Narojay.Blog.Domain.Models.Entity;
using Narojay.Blog.Infrastruct.DataBase;
using Narojay.Blog.Infrastruct.Extension;
using Narojay.Tools.Core.Dto;

namespace Narojay.Blog.Application.Service;

public class WebsiteEventLogService :IWebsiteEventLogService
{
    private readonly BlogContext _blogContext;
    private readonly IMapper _mapper;

    public WebsiteEventLogService(BlogContext blogContext,IMapper mapper)
    {
        _blogContext = blogContext;
        _mapper = mapper;
    }
    public async Task<PageOutputDto<WebsiteEventLogOutputDto>> GetPagingWebsiteEventLogAsync(WebsiteEventLogQuery pageInputDto)
    {
        var queryable =  _blogContext.WebsiteEventLogs.AsNoTracking()
            .Where(x =>  x.IsDeleted == false)
            .WhereIf(!string.IsNullOrEmpty(pageInputDto.Content) , x => x.Content.Contains(pageInputDto.Content));

        var queryableResult =await  queryable.PageBy(pageInputDto.PageIndex, pageInputDto.PageSize).ToListAsync();
        
        var  websiteEventLogOutputDtos = _mapper.Map<List<WebsiteEventLogOutputDto>>(queryableResult);

        return new PageOutputDto<WebsiteEventLogOutputDto>{Data = websiteEventLogOutputDtos,TotalCount = await queryable.CountAsync()};
        
    }

    public async Task<bool> RemoveWebsiteEventLogAsync(int id)
    {
        var websiteEventLog =await _blogContext.WebsiteEventLogs.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == id);

        if (websiteEventLog == null)
        {
            throw new StringResponseException("该条日志已删除或者不存在");
        }
        websiteEventLog.IsDeleted = true;

        return  await _blogContext.SaveChangesAsync() > 0;

    }

    public async Task<bool> UpdateWebsiteEventLogAsync(WebsiteEventLogDto websiteEventLogDto)
    {
        var websiteEventLog =await _blogContext.WebsiteEventLogs.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == websiteEventLogDto.Id);
        if (websiteEventLog == null )
        {
            throw new StringResponseException("日志已删除或者不存在");
        }
        websiteEventLog.Content = websiteEventLogDto.Content;
        websiteEventLog.LastModifyTime = DateTime.Now;

        return await _blogContext.SaveChangesAsync() > 0;
    }

    public async  Task<bool> AddWebsiteEventLogAsync(WebsiteEventLogDto websiteEventLogDto)
    {
      var websiteEventLog =  new WebsiteEventLog
        {
            Content = websiteEventLogDto.Content,
            IsDeleted = false,
            CreationTime = DateTime.Now
        };
      
      await _blogContext.WebsiteEventLogs.AddAsync(websiteEventLog);

      return  await _blogContext.SaveChangesAsync() > 0;
    }
}