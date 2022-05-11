using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Narojay.Blog.Models.Api;

namespace Narojay.Blog.Aop
{
    public class FormatResponseAttribute : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            {
                if (context?.Result != null)
                {
                    if (context.Result is ObjectResult objectResult)
                    {
                        context.Result = new JsonResult(new ApiResult
                        {
                            IsSuccess = true,
                            Code = objectResult.StatusCode ?? context.HttpContext.Response.StatusCode,
                            Message = "success",
                            Data = objectResult?.Value
                        });
                    }
                    else if (context.Result is EmptyResult)
                    {
                        context.Result = new JsonResult(new ApiResult
                            { Code = context.HttpContext.Response.StatusCode, Message = "success", Data = new { } });
                    }
                    else if (context.Result is ContentResult result)
                    {
                        context.Result = new JsonResult(new ApiResult
                        {
                            Code = context.HttpContext.Response.StatusCode,
                            Message = "success",
                            Data = result?.Content
                        });
                    }
                    else if (context.Result is FileResult)
                    {
                    }
                    else
                    {
                        throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
                    }
                }

                await next();
            }
        }
    }
}