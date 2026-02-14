(bool Success, string Host, int Port, string Database, string Error) ParseConnectionString(
    string connectionString
)
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        var elements = connectionString.Split(';').ToList();
        var keys = Enum.GetValues<ConnectionStringKey>().ToList();

        var dict = elements.Select(e => e.Split('=', 2)).ToDictionary(kv => kv[0], kv => kv[1]);

        if (keys.All(k => dict.ContainsKey(k.ToString())))
        {
            var host = dict[ConnectionStringKey.Server.ToString()];
            var port = dict[ConnectionStringKey.Port.ToString()];
            var database = dict[ConnectionStringKey.Database.ToString()];

            if (!int.TryParse(port, out int p))
                return (false, string.Empty, 0, string.Empty, "Invalid port format");

            return (true, host, p, database, string.Empty);
        }
    }

    return (false, string.Empty, 0, string.Empty, "Incorrect string");
}

(bool, string, int, string, string) goodString = ParseConnectionString("Server=localhost;Port=5432;Database=mydb");
Console.WriteLine("---- Start ParseConnectionString Method test");
Console.WriteLine($"Is success: {goodString.Item1}");
Console.WriteLine($"Host: {goodString.Item2}");
Console.WriteLine($"Port: {goodString.Item3}");
Console.WriteLine($"Database: {goodString.Item4}");
Console.WriteLine($"Error message: {goodString.Item5}");

Console.WriteLine("*****");
(bool, string, int, string, string) badString = ParseConnectionString(string.Empty);
Console.WriteLine($"Is success: {badString.Item1}");
Console.WriteLine($"Host: {badString.Item2}");
Console.WriteLine($"Port: {badString.Item3}");
Console.WriteLine($"Database: {badString.Item4}");
Console.WriteLine($"Error message: {badString.Item5}");

Console.WriteLine("---- Finish ParseConnectionString Method test");

enum ConnectionStringKey
{
    Server,
    Port,
    Database,
}
