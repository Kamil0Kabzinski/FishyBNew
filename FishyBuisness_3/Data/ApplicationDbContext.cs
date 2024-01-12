using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FishyBuisness_3.Models;

namespace FishyBuisness_3.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}
	    public DbSet<FishyBuisness_3.Models.Fish> Fish { get; set; } = default!;
        public DbSet<FishyBuisness_3.Models.ApplicationUser> ApplicationUsers { get; set; } = default!;
        public DbSet<FishyBuisness_3.Models.FishTank> FishTanks { get; set; } = default!;
        public DbSet<FishyBuisness_3.Models.Stock> Stocks { get; set; } = default!;
        public DbSet<FishyBuisness_3.Models.Environment> Environments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Konfiguracja relacji między WarehouseItem, Product i Warehouse
            modelBuilder.Entity<Stock>()
                .HasOne(wi => wi.Fish)
                .WithMany()
                .HasForeignKey(wi => wi.FishId)
                .OnDelete(DeleteBehavior.NoAction); 

            modelBuilder.Entity<Stock>()
                .HasOne(wi => wi.FishTank)
                .WithMany()
                .HasForeignKey(wi => wi.FishTankId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Stock>()
                .HasOne(wi => wi.Environment)
                .WithMany()
                .HasForeignKey(wi => wi.EnvironmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Fish>()
                .HasOne(wi => wi.Environment)
                .WithMany()
                .HasForeignKey(wi => wi.EnvironmentId);

            modelBuilder.Entity<FishTank>()
                .HasOne(wi => wi.Environment)
                .WithMany()
                .HasForeignKey(wi => wi.EnvironmentId);

            base.OnModelCreating(modelBuilder);

            


        }
    }
}
