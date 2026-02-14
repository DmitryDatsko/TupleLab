namespace TupleLab;

public static class ParsingValidation
{
    enum ConnectionStringKey
    {
        Server,
        Port,
        Database,
    }

    public static (
        bool Success,
        string Host,
        int Port,
        string Database,
        string Error
    ) ParseConnectionString(string connectionString)
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
}
