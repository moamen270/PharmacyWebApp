using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PharmacyWebApp.Models;

namespace PharmacyWebApp.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }

        private DbSet<Product> Products { get; set; }
        private DbSet<Brand> Brands { get; set; }
        private DbSet<Category> Categories { get; set; }
        private DbSet<ApplicationUser> ApplicationUsers { get; set; }
        private DbSet<Review> Reviews { get; set; }
        private DbSet<ShopCart> ShopCarts { get; set; }
        private DbSet<CreditCard> CreditCards { get; set; }
        private DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<OrderForDetail> OrderDetail { get; set; }
        public DbSet<OrderForHeader> OrderHeaders { get; set; }
    }
}