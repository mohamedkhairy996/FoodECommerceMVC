using DomainLayerCore.Interfaces;
using DomainLayerCore.Models;
using InfrastructureLayerEF;
using InfrastructureLayerEF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ModelClasses;


namespace ECommerce
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDBContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddSignalR();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => { options.SignIn.RequireConfirmedAccount = true;
            options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
              
            }).AddEntityFrameworkStores<ApplicationDBContext>();

            builder.Services.AddSession(Options => {
                Options.IdleTimeout = TimeSpan.FromSeconds(10);
                Options.Cookie.HttpOnly = true;
                Options.Cookie.IsEssential = true;
            });
                 


            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

            //builder.Services.AddScoped<Repository.GenericRepository<Category>,Repository.CategoryRepository>();
            //builder.Services.AddScoped<Repository.GenericRepository<Product>, Repository.ProductRepository>();
            builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
            //builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));

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

            app.MapHub<ChatHub>("/chatHub");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Product}/{action=All}/{id?}");
            //app.MapRazorPages();

            app.Run();
        }
    }
}