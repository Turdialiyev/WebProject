using Bulky.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Save", DisplayOrder = 2 },
                new Category { Id = 3, Name = "History", DisplayOrder = 3 });

            modelBuilder.Entity<Product>().HasData(
                new Product 
                {
                    Id= 1,
                    Title = "Iphone 11 pro max",
                    Author = "Hamid Olimjon",
                    ISBN = "SN9099",
                    Description= "Description",
                    ListPrice = 90,
                    Price50 = 47,
                    Price = 789,
                    Price100= 100,
                    CategoryId= 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 2,
                    Title = "Iphone 11 pro max",
                    Author = "Hamid Olimjon",
                    ISBN = "SN9099",
                    Description = "Description",
                    ListPrice = 90,
                    Price50 = 47,
                    Price = 789,
                    Price100 = 100,
                    CategoryId= 1,
                    ImageUrl = ""
                },
                new Product
                {
                    Id = 3,
                    Title = "Iphone 11 pro max",
                    Author = "Hamid Olimjon",
                    ISBN = "SN9099",
                    Description = "Description",
                    ListPrice = 90,
                    Price50 = 47,
                    Price = 789,
                    Price100 = 100,
                    CategoryId = 2,
                    ImageUrl=""
                }

                );


        }



    }
}
