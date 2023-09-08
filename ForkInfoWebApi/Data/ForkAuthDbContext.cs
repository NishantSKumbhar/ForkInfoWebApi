using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForkInfoWebApi.Data
{
    public class ForkAuthDbContext: IdentityDbContext
    {
        public ForkAuthDbContext(DbContextOptions<ForkAuthDbContext> dbContextOptions): base(dbContextOptions)
        {

           
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleID = "ae9e5415-8c37-4141-ae10-d18a70b69452";
            var writerRoleID = "08cce3ba-5669-4494-a98e-859df748636f";

            var roles = new List<IdentityRole>
            {
                new IdentityRole()
                {
                    Id= readerRoleID,
                    Name = "Reader",
                    NormalizedName= "Reader".ToUpper(),
                    ConcurrencyStamp = readerRoleID
                },
                new IdentityRole()
                {
                    Id= writerRoleID,
                    Name = "Writer",
                    NormalizedName= "Writer".ToUpper(),
                    ConcurrencyStamp = writerRoleID
                }
            };

            builder.Entity<IdentityRole>().HasData(roles);

            // ADMIN
            var adminUserId = "9eb119a2-da36-4a7e-bf09-7105f48cafd6";
            var admin = new IdentityUser()
            {
                Id = adminUserId,
                UserName = "admin",
                Email= "admin@admin.com",
                NormalizedEmail = "admin@admin.com".ToUpper(),
                NormalizedUserName = "admin".ToUpper()
            };

            admin.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(admin, "Admin@123");

            builder.Entity<IdentityUser>().HasData(admin);

            // Give Roles to admin
            var adminRoles = new List<IdentityUserRole<string>>()
            {
                new()
                {
                    UserId = adminUserId,
                    RoleId = readerRoleID
                },
                new()
                {
                    UserId = adminUserId,
                    RoleId = writerRoleID
                }
            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);
        }

    }
}
