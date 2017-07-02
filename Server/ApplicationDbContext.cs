using FoxMoney.Server.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoxMoney.Server
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<SecurityPrice> SecurityPrices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            modelBuilder.Entity<Portfolio>()
                .Property(p => p.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<Portfolio>()
                .Property(p => p.Draft)
                .HasDefaultValue(false);
            modelBuilder.Entity<Security>()
                .Property(s => s.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<Holding>()
                .Property(h => h.HoldingClosed)
                .HasDefaultValue(false);
            modelBuilder.Entity<Parcel>()
                .Property(p => p.ParcelExhausted)
                .HasDefaultValue(false);
            modelBuilder.Entity<SecurityPrice>()
                .HasIndex(s => s.Date);
            modelBuilder.Entity<HoldingTransaction>();
            modelBuilder.Entity<HoldingIncome>();
        }
    }
}