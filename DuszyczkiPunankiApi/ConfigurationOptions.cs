namespace MikietaApi;

public class ConfigurationOptions
{
    public string Database { get; }

    public ConfigurationOptions(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Database = environment.IsDevelopment()
            ? configuration["ConnectionStrings:Database"]!
            //DATABASE_URL - key  for heroku environment variable
            : ConvertPostgresConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL")!);
    }

    public static string ConvertPostgresConnectionString(string postgresUrl)
    {
        try
        {
            var uri = new Uri(postgresUrl);
            var userInfo = uri.UserInfo.Split(':');

            var host = uri.Host;
            var port = uri.Port;
            var database = uri.AbsolutePath.TrimStart('/');
            var username = userInfo[0];
            var password = userInfo[1];

            return
                $"Host={host};Port={port};Database={database};Username={username};Password={password};SSL Mode=Require;Trust Server Certificate=true";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing connection string: {ex.Message}");
            return null;
        }
    }
}