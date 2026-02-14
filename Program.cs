using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TupleLab;
using TupleLab.ApiDb;

(bool, string, int, string, string) goodString = ParsingValidation.ParseConnectionString(
    "Server=localhost;Port=5432;Database=mydb"
);
Console.WriteLine("---- Start ParseConnectionString Method test ----");
Console.WriteLine($"Is success: {goodString.Item1}");
Console.WriteLine($"Host: {goodString.Item2}");
Console.WriteLine($"Port: {goodString.Item3}");
Console.WriteLine($"Database: {goodString.Item4}");
Console.WriteLine($"Error message: {goodString.Item5}");

Console.WriteLine("*****");
(bool, string, int, string, string) badString = ParsingValidation.ParseConnectionString(string.Empty);
Console.WriteLine($"Is success: {badString.Item1}");
Console.WriteLine($"Host: {badString.Item2}");
Console.WriteLine($"Port: {badString.Item3}");
Console.WriteLine($"Database: {badString.Item4}");
Console.WriteLine($"Error message: {badString.Item5}");

Console.WriteLine("---- Finish ParseConnectionString Method test ----");

Console.WriteLine("---- Start ProductService test ----");
var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite("Data Source=products.db"));
services.AddScoped<ProductService>();
var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();

var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
var (products, total, filtered, filters, time) = await productService.SearchProductsAsync("s", 1.5M, null);

foreach(var p in products)
{
    Console.WriteLine($"Product: {p.Name}");
}

Console.WriteLine("---- Finish ProductService test ----");
