using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Narojay.Blog.Aop;
using Narojay.Blog.Models.Api;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Narojay.Blog.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IWebHostEnvironment _environment;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = context.Response.StatusCode == 200 ? (int)HttpStatusCode.BadRequest : context.Response.StatusCode;
                var message = "操作失败";
                if (ex is StringResponseException)
                {
                    statusCode = 200;
                    message = ex.Message;
                }
                else
                {
                    //Log.Logger.Error(ex, "1");
                }

                await HandleExceptionAsync(context, statusCode, message);
            }
            finally
            {
                var statusCode = context.Response.StatusCode;
                var msg = "";
                if (statusCode == 401)
                {
                    msg = "未授权";
                }
                else if (statusCode == 404)
                {
                    msg = "未找到服务";
                }
                else if (statusCode == 502)
                {
                    msg = "请求错误";
                }
                else if (statusCode != 200)
                {
                    msg = "未知错误";
                }
                if (!string.IsNullOrWhiteSpace(msg))
                {
                    await HandleExceptionAsync(context, statusCode, msg);
                }
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new ApiResult { Code = statusCode.ToString(), IsSuccess = false, Message = msg };
            var result = JsonConvert.SerializeObject(data);
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }

    }
}