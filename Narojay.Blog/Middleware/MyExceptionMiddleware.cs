﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Narojay.Blog.Models.Api;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Narojay.Blog.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment)
        {
            _next = next;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                //var features = context.Features;
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private async Task HandleException(HttpContext context, Exception e)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "text/json;charset=utf-8;";
            if (_environment.IsDevelopment())
            {
                var json = new { message = e.Message };
                var error = JsonConvert.SerializeObject(json);
                await context.Response.WriteAsync(error);
            }
            else
            {
                var result = JsonConvert.SerializeObject(new ApiResult
                {
                    Code = context.Response.StatusCode,
                    Message = "操作失败",
                    IsSuccess = false
                });
                await context.Response.WriteAsync(result);
            }
        }
    }

}
