using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TupleLab;
using TupleLab.ApiDb;
using TupleLab.PatternMatching;

(bool, string, int, string, string) rightString = ParsingValidation.ParseConnectionString(
    "Server=localhost;Port=5432;Database=mydb"
);
Console.WriteLine("---- Connection String Parser ---");
Console.WriteLine(
    $"Success: {rightString.Item1}\nHost: {rightString.Item2}, Port: {rightString.Item3}, Database: {rightString.Item4}"
);
Console.WriteLine("*****");
(bool, string, int, string, string) badString = ParsingValidation.ParseConnectionString(
    string.Empty
);
Console.WriteLine(
    $"Success: {badString.Item1}\nHost: {badString.Item2}, Port: {badString.Item3}, Database: {badString.Item4}"
);

Console.WriteLine();

Console.WriteLine("---- Product Search ----");
var services = new ServiceCollection();

services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=products.db"));
services.AddScoped<ProductService>();
var provider = services.BuildServiceProvider();

using var scope = provider.CreateScope();

var productService = scope.ServiceProvider.GetRequiredService<ProductService>();
var (products, totalCount, filteredCount, filters, time) = await productService.SearchProductsAsync(
    "s",
    1.5M,
    null
);

Console.WriteLine($"Found {filteredCount} products");
Console.WriteLine($"Filters: {filters}");
Console.WriteLine($"Execution time: {time} ms");

Console.WriteLine();
Console.WriteLine("---- Access Control Service ----");

var admin = AccessControlService.CheckAccess(
    AccessControlService.Role.Admin,
    AccessControlService.Resource.AdminPanel,
    false
);

var user = AccessControlService.CheckAccess(
    AccessControlService.Role.User,
    AccessControlService.Resource.AdminPanel,
    false
);

Console.WriteLine(
    $"For admin request: HasAccess: {admin.HasAccess}, Reason: {admin.Reason}, Status code: {admin.StatusCode}"
);
Console.WriteLine(
    $"For user request: HasAccess: {user.HasAccess}, Reason: {user.Reason}, Status code: {user.StatusCode}"
);
