using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleProductDataModels;

namespace SimpleProductData;

public class SimpleProductDataDbContext : DbContext
{
    private static IConfigurationRoot _configuration;

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<NewProductStaging> NewProducts { get; set; }

    //Add a default constructor if scaffolding is needed
    public SimpleProductDataDbContext() { }

    //Add the complex constructor for allowing Dependency Injection
    public SimpleProductDataDbContext(DbContextOptions options)
        : base(options)
    {
        //intentionally empty. 
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
            var cnstr = _configuration.GetConnectionString("InventoryManager");
            optionsBuilder.UseSqlServer(cnstr);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Category>(x =>
        {
            x.HasData(
                new Category() { Id = 1, Name = "Kitchen", Description="Kitchen and Cooking" },
                new Category() { Id = 2, Name = "Bath", Description = "Shower, bath, toilet, towels" },
                new Category() { Id = 3, Name = "Living", Description = "Furniture and Accessories" },
                new Category() { Id = 4, Name = "Decor", Description = "Art, plants, frames, candles" },
                new Category() { Id = 5, Name = "Outdoor", Description = "Grills, Patio, Lawn" }
            );
        });
    }
}