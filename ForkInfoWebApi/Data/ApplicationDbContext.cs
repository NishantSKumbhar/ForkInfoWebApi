using ForkInfoWebApi.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ForkInfoWebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Contact> Contact { get; set; }
    }
}
