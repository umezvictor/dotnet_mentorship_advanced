using CatalogService;
using DAL.Database;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Catalog.IntegrationTest;

public class CatalogWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("TestDb")
            );

            //test data 
            var sp = services.BuildServiceProvider();
            using (var scope = sp.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                dbContext.Category.AddRange(new Category
                {
                    Id = 1,
                    Name = "Category One",
                    Image = "category-one-image-url"
                },
                new Category
                {
                    Id = 2,
                    Name = "Category Two",
                    Image = "category-two-image-url"
                });

                dbContext.SaveChanges();

                dbContext.Products.AddRange(new Product
                {
                    Id = 1,
                    Name = "Seeded Product",
                    Description = "Seeded Description",
                    Price = 29.99M,
                    Amount = 10,
                    CategoryId = 1,
                    Image = "seeded-image-url"
                }, new Product
                {
                    Id = 2,
                    Name = "Product Two",
                    Description = "Description for Product Two",
                    Price = 20.50M,
                    Amount = 8,
                    CategoryId = 1,
                    Image = "image-two-url"
                }, new Product
                {
                    Id = 3,
                    Name = "Product Three",
                    Description = "Description for Product Three",
                    Price = 15.75M,
                    Amount = 12,
                    CategoryId = 2,
                    Image = "image-three-url"
                },
                new Product
                {
                    Id = 4,
                    Name = "Product Four",
                    Description = "Description for Product Four",
                    Price = 5.00M,
                    Amount = 20,
                    CategoryId = 2,
                    Image = "image-four-url"
                });
                dbContext.SaveChanges();
            }
        });
    }
}
