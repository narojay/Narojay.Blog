using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Models;

namespace Narojay.Blog.Infrastructure
{
    public class DataContext : DbContext
    {

        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine)
                .UseLazyLoadingProxies()
                .EnableDetailedErrors()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }
        public DbSet<Student> Students { get; set; }  
        public DbSet<BlogUser> BlogUsers { get; set; }  
        public DbSet<Comment> Comments { get; set; }
    }
}
