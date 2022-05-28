using System;
using Microsoft.EntityFrameworkCore;
using Narojay.Blog.Models.Entity;
using Narojay.Blog.Models.Entity.Test;
using Serilog;

namespace Narojay.Blog.Infrastructure
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options)
        {
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<PostTags> PostTags { get; set; }
        public DbSet<Soliloquize> Soliloquizes { get; set; }
        public DbSet<TestUser> TestUsers { get; set; }
        public DbSet<AdminNotice> AdminNotices { get; set; }
        public DbSet<TestAccount> TestAccounts { get; set; }
        public DbSet<LeaveMessage> LeaveMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(Log.Information)
                .EnableDetailedErrors()
                .EnableSensitiveDataLogging()
                .UseLazyLoadingProxies()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PostId);
            modelBuilder.Entity<User>().HasMany(x => x.Posts).WithOne(x => x.User).HasForeignKey(x => x.UserId);
            //modelBuilder.Entity<LeaveMessage>().HasMany(e => e.Children).WithOne(c => c.Parent)
            //    .HasForeignKey(c => c.ParentId).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<User>().HasOne(x => x.TestAccount).WithOne()
                .HasForeignKey<TestAccount>(x => x.UserId);

            modelBuilder.Entity<PostTags>().HasOne(x => x.Tag).WithMany()
                .HasForeignKey(x => x.TagId);
        }
    }
}