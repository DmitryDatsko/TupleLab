using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace TupleLab.ApiDb;

public class ProductService
{
    private readonly AppDbContext _dbContext;

    public ProductService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
        _dbContext.Database.Migrate();
    }

    public async Task<(
        List<Product> Products,
        int TotalCount,
        int FilteredCount,
        string AppliedFilters,
        TimeSpan ExecutionTime
    )> SearchProductsAsync(string searchTerm, decimal? minPrice, decimal? maxPrice)
    {
        var watch = Stopwatch.StartNew();

        IQueryable<Product> query = (minPrice.HasValue, maxPrice.HasValue) switch
        {
            (true, true) => _dbContext.Products.Where(p =>
                p.Price > minPrice && p.Price < maxPrice
            ),
            (false, true) => _dbContext.Products.Where(p => p.Price < maxPrice),
            (true, false) => _dbContext.Products.Where(p => p.Price > minPrice),
            _ => _dbContext.Products,
        };

        var products = await query.Where(p => p.Name.Contains(searchTerm)).ToListAsync();
        watch.Stop();

        string appliedFilters =
            $"searchTerm: {searchTerm}, "
            + $"{(minPrice.HasValue ? $"minPrice: {minPrice.Value}" : "")}"
            + $"{(maxPrice.HasValue ? $", maxPrice: {maxPrice.Value}" : "")}";

        return (
            products,
            await _dbContext.Products.CountAsync(),
            products.Count,
            appliedFilters,
            watch.Elapsed
        );
    }
}
