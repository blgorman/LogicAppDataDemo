using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleProductData;
using SimpleProductDataViewer.Data;

namespace SimpleProductDataViewer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            var productDataCNSTR = builder.Configuration.GetConnectionString("SimpleProductDataDb") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<SimpleProductDataDbContext>(options =>
                options.UseSqlServer(productDataCNSTR));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                                        .UseSqlServer(connectionString)
                                        .Options;
            using (var context = new ApplicationDbContext(contextOptions))
            {
                context.Database.Migrate();
            }

            var contextOptions2 = new DbContextOptionsBuilder<SimpleProductDataDbContext>()
                .UseSqlServer(productDataCNSTR)
                .Options;
            using (var context = new SimpleProductDataDbContext(contextOptions2))
            {
                context.Database.Migrate();
            }

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}