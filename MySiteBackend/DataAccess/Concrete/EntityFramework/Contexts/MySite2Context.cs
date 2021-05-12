using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework.Contexts
{
    public class MySite2Context : IdentityDbContext<User, Role, string>
    {
        public MySite2Context(DbContextOptions<MySite2Context> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogTag>().HasKey(bt => new { bt.BlogId, bt.TagId });
            modelBuilder.Entity<User>()
            .HasMany<Comment>(c => c.Comments)
            .WithOne(s => s.User)
             .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BlogTag> BlogTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<SiteInformation> SiteInformations { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Ministration> Ministrations { get; set; }
        public DbSet<Log> Logs { get; set; }

        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
    }
}
