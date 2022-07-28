using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Narojay.Blog.Domain;
using Narojay.Blog.Domain.Models.Api;
using Newtonsoft.Json;
using Serilog;

namespace Narojay.Blog.Middleware;

public class ExceptionMiddleware
{
    private readonly IWebHostEnvironment _environment;
    private readonly ILogger _logger;
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
            context.Response.ContentType = "text/json;charset=utf-8;";
            var statusCode = context.Response.StatusCode == 200
                ? (int)HttpStatusCode.BadRequest
                : context.Response.StatusCode;
            var message = "操作失败";
            if (ex is StringResponseException)
            {
                context.Response.StatusCode = 209;
                statusCode = 488;
                message = ex.Message;
            }
            else
            {
                Log.Error(ex, "系统异常");
            }

            await HandleExceptionAsync(context, statusCode, message);
        }
    }


    private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
    {
        var data = new ApiResult { Code = statusCode.ToString(), IsSuccess = false, Message = msg };
        var result = JsonConvert.SerializeObject(data);
        return context.Response.WriteAsync(result);
    }
}