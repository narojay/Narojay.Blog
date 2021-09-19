using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Narojay.Blog.Models;
using System;
using Narojay.Blog.Models.Entity;

namespace Narojay.Blog.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<LeaveMessage> LeaveMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
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
            modelBuilder.Entity<Comment>().HasOne(x => x.Post).WithMany(x => x.Comments).HasForeignKey(x => x.PostId);
            modelBuilder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<User>().HasMany(x =>x.LeaveMessages).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }


}
