using Autofac;
using CSRedis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Narojay.Blog.Configs;
using Narojay.Blog.Extensions;
using Narojay.Blog.Infrastructure;
using System;
using System.Collections.Generic;
using Hangfire;
using Hangfire.MySql;
using Hangfire.States;
using Narojay.Blog.Infrastructure.Service;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Narojay.Blog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppConfig.Redis = configuration[nameof(AppConfig.Redis)];
            AppConfig.ConnString = configuration[nameof(AppConfig.ConnString)];
            AppConfig.JwtSecret = configuration[nameof(AppConfig.JwtSecret)];
            AppConfig.JwtValid = configuration[nameof(AppConfig.JwtValid)];
            Configuration.GetSection("Sign");
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Test>("123",x => x.X = "1");
            services.Configure<Test>("456",x => x.X = "2");
            services.AddMapper();
            services.AddSignalR();
            //services.AddHangfire(x => x.UseStorage(new MySqlStorage(AppConfig.ConnString, new MySqlStorageOptions
            //{
            //    TablesPrefix = "blog"
            //}))).AddHangfireServer(x =>
            //{
            //    x.ServerName = "blog.server";
            //    x.Queues = new[] { EnqueuedState.DefaultQueue, "blog_job" };
            //    x.WorkerCount = 10;
            //});
            var processorCount = Environment.ProcessorCount;
            services.AddHttpContextAccessor();
            RedisHelper.Initialization(new CSRedisClient(AppConfig.Redis));
            services.AddControllers().AddControllersAsServices().AddNewtonsoftJson(option =>
               //ºöÂÔÑ­»·ÒýÓÃ
               option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<DataContext>((opt) =>
            {
                opt.UseMySql(AppConfig.ConnString, ServerVersion.AutoDetect(AppConfig.ConnString));
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Narojay.Blog", Version = "v1" });
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                c.OperationFilter<SecurityRequirementsOperationFilter>();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,                                
                    Type = SecuritySchemeType.ApiKey,
                    Description = "",
                    Name = "Authorization",
                });
             
          

            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(option =>
            {
                option.RequireHttpsMetadata = false;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = AppConfig.JwtValid,
                    ValidateAudience = true,
                    ValidAudience = AppConfig.JwtValid,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(AppConfig.JwtSecret)),
                    ValidateLifetime = true,
                    SaveSigninToken = true,
                };
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }

     
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            Console.WriteLine(env.EnvironmentName);
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Narojay.Blog v1"));
            //app.HangFireStart();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TestHub>("/testHub");
            });
        }
    }
}
