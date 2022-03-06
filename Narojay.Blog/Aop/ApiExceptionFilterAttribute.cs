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
            context.Result =new JsonResult(BuildExceptionResult(context.Exception));
            return base.OnExceptionAsync(context);
        }

        private ApiResult BuildExceptionResult(Exception exception)
        {
            if (exception is ApplicationException)
            {
                return new ApiResult
                {
                    Code = 200,
                    IsSuccess = false,
                    Message = exception.Message,
                };
            }

            return new ApiResult
            {
                Code = 500,
                IsSuccess = false,
                Message = "系统异常",
            };
        }

    }
}
