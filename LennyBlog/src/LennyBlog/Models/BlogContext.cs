using LennyBlog.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LennyBlog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions option) : base(option)
        {

        }

        public DbSet<Article> Articles { set; get; }

        public DbSet<Category> Categories { set; get; }

        public DbSet<User> Users { set; get; }

        public DbSet<Reply> Replies { set; get; }

        public DbSet<Carousel> Carousels { get; set; }

        public DbSet<Link> Links { set; get; }

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             //IConfiguration Configuration = ConfigurationHelper.GetConfiguration("appsettings.json");
             //optionsBuilder.UseNpgsql(Configuration.GetConnectionString("PostgreSql"));
             optionsBuilder.UseNpgsql("User ID=postgres;Password=123456;Host=localhost;Port=5432;Database=blog");
             base.OnConfiguring(optionsBuilder);
         }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
