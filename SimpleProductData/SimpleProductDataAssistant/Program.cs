using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleProductData;

namespace SimpleProductDataAssistant;

public class Program
{
    private static IConfigurationRoot _configuration;
    private static DbContextOptionsBuilder<SimpleProductDataDbContext> _optionsBuilder;

    public static void Main(string[] args)
    {
        BuildOptions();
        MigrateDatabase();
    }

    private static void BuildOptions()
    {
        _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
        _optionsBuilder = new DbContextOptionsBuilder<SimpleProductDataDbContext>();
        _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SimpleProductDataDb"));
    }

    private static void MigrateDatabase()
    {
        using (var db = new SimpleProductDataDbContext(_optionsBuilder.Options))
        {
            db.Database.Migrate();
        }
    }
}