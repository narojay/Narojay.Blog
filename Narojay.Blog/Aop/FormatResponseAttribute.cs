using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Narojay.Blog.Models.Api;
using System;

namespace Narojay.Blog.Aop
{
    public class FormatResponseAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // 返回结果为JsonResult的请求进行Result包装
            if (context?.Result != null)
            {
                if (context.Result is ObjectResult objectResult)
                {
                    context.Result = new JsonResult(new ApiResult
                        { Code = context.HttpContext.Response.StatusCode, Message = "success", Data = objectResult?.Value });

                }
                else if (context.Result is EmptyResult)
                {
                    context.Result = new JsonResult(new ApiResult { Code = context.HttpContext.Response.StatusCode, Message = "success", Data = new { } });
                }
                else if (context.Result is ContentResult result)
                {
                    context.Result = new JsonResult(new ApiResult
                        { Code = context.HttpContext.Response.StatusCode, Message = "success", Data = result?.Content });
                }
                else
                {
                    throw new Exception($"未经处理的Result类型：{context.Result.GetType().Name}");
                }

            }
        }
    }
}
