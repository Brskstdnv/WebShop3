using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WebShopApp.Data;
using WebShopApp.Infrastrucutre.Data.Domain;

namespace WebShopApp.Infrastrucutre.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            var services = serviceScope.ServiceProvider;

            await RoleSeeder(services);
            await SeedAdministrator(services);
            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories (dataCategory);
            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrand(dataBrand);

            return app;
        }
        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if (dataCategory.Categories.Any())
            {
                return;
            }

            dataCategory.Categories.AddRange(new[]
            {
            new Category {CategoryName="Analogue"},
            new Category {CategoryName="Digital"},
            new Category {CategoryName="Chronograph"},
            new Category {CategoryName="Hybryd"},
            new Category {CategoryName="Smart watch"}
            });

            dataCategory.SaveChanges();
        }

        private static void SeedBrand(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }

            dataBrand.Brands.AddRange(new[]
            {
            new Brand {BrandName="Rolex"},
            new Brand {BrandName="Hublot"},
            new Brand {BrandName="IWC"},
            new Brand {BrandName="Omega"},
            new Brand {BrandName="Casio"},
            new Brand {BrandName="Cartier"},
            new Brand {BrandName="Patek Philipe"},
            new Brand {BrandName="AP"}
            });

            dataBrand.SaveChanges();
        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administrator", "Client" };
            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdministrator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "Admin";
                user.LastName = "Adminov";
                user.UserName = "ADMIN";
                user.Email = "admin@adminski.com";
                user.Adress = "admin address";
                user.PhoneNumber = "0888888888";

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

    }
}
