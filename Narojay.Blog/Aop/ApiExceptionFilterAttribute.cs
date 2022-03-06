using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Narojay.Blog.Models.Api;
using System;
using System.Threading.Tasks;

namespace Narojay.Blog.Aop
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(ExceptionContext context)
        {
            // logger.log
            context.Result =new JsonResult(BuildExceptionResult(context));
            return base.OnExceptionAsync(context);
        }

        private ApiResult BuildExceptionResult(ExceptionContext context)
        {
            var statusCode = context.HttpContext.Response.StatusCode;

            var apiResult = new ApiResult
            {
                Code = statusCode,
                IsSuccess = false
            };
            if (context.Exception is ApplicationException)
            {
                return new ApiResult
                {
                    Code = statusCode,
                    IsSuccess = false,
                    Message = context.Exception.Message,
                };
            }
            else if (statusCode == StatusCodes.Status401Unauthorized)
            {
                apiResult.Message = "未授权";
            }
            else if (statusCode == StatusCodes.Status404NotFound)
            {
                apiResult.Message = "未找到服务";
            }
            else if (statusCode == StatusCodes.Status502BadGateway)
            {
                apiResult.Message = "网关错误";
            }
            else
            {
                apiResult.Message = "未知错误";
            }
            // logger.log(context)
            return apiResult;
        }

    }
}
