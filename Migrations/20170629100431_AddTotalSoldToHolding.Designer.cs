using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FoxMoney.Server;
using FoxMoney.Server.Entities;

namespace FoxMoney.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20170629100431_AddTotalSoldToHolding")]
    partial class AddTotalSoldToHolding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.2");

            modelBuilder.Entity("FoxMoney.Server.Entities.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Description")
                        .HasMaxLength(250);

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .HasMaxLength(250);

                    b.Property<bool>("IsEnabled");

                    b.Property<string>("LastName")
                        .HasMaxLength(250);

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Holding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<string>("CustomName");

                    b.Property<bool>("HoldingClosed")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("PortfolioId");

                    b.Property<decimal>("RealisedGain");

                    b.Property<int>("SecurityId");

                    b.Property<int?>("SecurityPriceId");

                    b.Property<decimal>("TotalCostBase");

                    b.Property<decimal>("TotalIncome");

                    b.Property<int>("TotalUnits");

                    b.Property<int>("TotalUnitsSold");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.HasIndex("SecurityId");

                    b.HasIndex("SecurityPriceId");

                    b.ToTable("Holding");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.HoldingIncome", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<long>("HoldingId");

                    b.Property<int?>("HoldingId1");

                    b.Property<decimal>("Income");

                    b.Property<DateTime>("IncomeDate");

                    b.Property<bool>("IncomeReinvested");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int?>("ReinvestmentHoldingTransactionId");

                    b.HasKey("Id");

                    b.HasIndex("HoldingId1");

                    b.HasIndex("ReinvestmentHoldingTransactionId");

                    b.ToTable("HoldingIncome");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.HoldingTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("Brokerage");

                    b.Property<long>("GeneratedParcelId");

                    b.Property<int?>("GeneratedParcelId1");

                    b.Property<int>("HoldingId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<DateTime>("TransactionDate");

                    b.Property<int>("TransactionType");

                    b.Property<decimal>("UnitPrice");

                    b.Property<int>("Units");

                    b.HasKey("Id");

                    b.HasIndex("GeneratedParcelId1");

                    b.HasIndex("HoldingId");

                    b.ToTable("HoldingTransaction");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Parcel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("Brokerage");

                    b.Property<int>("HoldingId");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<bool>("ParcelExhausted")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("PurchaseDate");

                    b.Property<decimal>("PurchasePrice");

                    b.Property<int>("UnitsPurchased");

                    b.Property<int>("UnitsSold");

                    b.HasKey("Id");

                    b.HasIndex("HoldingId");

                    b.ToTable("Parcel");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("CostBase");

                    b.Property<decimal>("CurrentIncome");

                    b.Property<decimal>("CurrentRawValue");

                    b.Property<bool>("Draft")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<int>("OwnerId");

                    b.Property<decimal>("TotalRealisedGain");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Security", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Active")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(true);

                    b.Property<DateTime>("AddedDate");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<string>("Name");

                    b.Property<string>("YahooCode");

                    b.HasKey("Id");

                    b.ToTable("Security");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.SecurityPrice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AddedDate");

                    b.Property<decimal>("ClosingPrice");

                    b.Property<DateTime>("Date");

                    b.Property<DateTime>("ModifiedDate");

                    b.Property<int>("SecurityId");

                    b.HasKey("Id");

                    b.HasIndex("Date");

                    b.HasIndex("SecurityId");

                    b.ToTable("SecurityPrices");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("Scope");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictScope", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AuthorizationId");

                    b.Property<string>("Subject");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Holding", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.Portfolio", "Portfolio")
                        .WithMany("Holdings")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoxMoney.Server.Entities.Security", "Security")
                        .WithMany()
                        .HasForeignKey("SecurityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoxMoney.Server.Entities.SecurityPrice", "SecurityPrice")
                        .WithMany()
                        .HasForeignKey("SecurityPriceId");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.HoldingIncome", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.Holding", "Holding")
                        .WithMany("Income")
                        .HasForeignKey("HoldingId1");

                    b.HasOne("FoxMoney.Server.Entities.HoldingTransaction", "ReinvestmentHoldingTransaction")
                        .WithMany()
                        .HasForeignKey("ReinvestmentHoldingTransactionId");
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.HoldingTransaction", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.Parcel", "GeneratedParcel")
                        .WithMany()
                        .HasForeignKey("GeneratedParcelId1");

                    b.HasOne("FoxMoney.Server.Entities.Holding", "Holding")
                        .WithMany("Transactions")
                        .HasForeignKey("HoldingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Parcel", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.Holding", "Holding")
                        .WithMany("Parcels")
                        .HasForeignKey("HoldingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.Portfolio", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("FoxMoney.Server.Entities.SecurityPrice", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.Security", "Security")
                        .WithMany()
                        .HasForeignKey("SecurityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("FoxMoney.Server.Entities.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoxMoney.Server.Entities.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictAuthorization", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Authorizations")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("OpenIddict.Models.OpenIddictToken", b =>
                {
                    b.HasOne("OpenIddict.Models.OpenIddictApplication", "Application")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.Models.OpenIddictAuthorization", "Authorization")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });
        }
    }
}
