using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Models;
using System;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<User> BlogUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public static readonly ILoggerFactory MyLoggerFactory
            = LoggerFactory.Create(builder => { builder.AddConsole(); });
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine)
                .EnableDetailedErrors()
                .UseLazyLoadingProxies()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }
    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


}
