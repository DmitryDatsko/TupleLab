using Microsoft.EntityFrameworkCore;

namespace TupleLab.ApiDb;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    public AppDbContext() { } // Only for testing purpose, don't cut my hands

    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite("Data Source=products.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Product>()
            .HasData(
                new Product
                {
                    Id = 1,
                    IsActive = true,
                    Name = "Apple",
                    Price = 5.2M,
                },
                new Product
                {
                    Id = 2,
                    IsActive = false,
                    Name = "PS5",
                    Price = 650,
                },
                new Product
                {
                    Id = 3,
                    IsActive = true,
                    Name = "Toothpick",
                    Price = 1.5M,
                },
                new Product
                {
                    Id = 4,
                    IsActive = true,
                    Name = "Lemon Tarts",
                    Price = 5.2M,
                },
                new Product
                {
                    Id = 5,
                    IsActive = true,
                    Name = "Cheese",
                    Price = 8.75M,
                },
                new Product
                {
                    Id = 6,
                    IsActive = false,
                    Name = "Flour",
                    Price = 23.18M,
                },
                new Product
                {
                    Id = 7,
                    IsActive = true,
                    Name = "Corn Syrup",
                    Price = 5.36M,
                },
                new Product
                {
                    Id = 8,
                    IsActive = true,
                    Name = "Ginsing",
                    Price = 45.8M,
                }
            );
    }
}
