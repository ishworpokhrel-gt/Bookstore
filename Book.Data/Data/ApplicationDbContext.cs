using Book.Models;
using Microsoft.EntityFrameworkCore;

namespace Book.DataAccess.Data
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Ishwor", OrderBy = 2 },
                new Category { Id = 2, Name = "Samundra", OrderBy = 3 },
                new Category { Id = 3, Name = "Riyaz", OrderBy = 4 }
                );
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 4,Author="blizzard",LeastPrice=200, Priceof50=5000,priceof100=7000,CategoryId=1,ImgUrl="" },
                new Product { Id = 5, Author = "Itachi", LeastPrice = 300, Priceof50 = 6000, priceof100 = 8000, CategoryId = 2, ImgUrl = "" },
                new Product { Id = 6, Author = "sasuke", LeastPrice = 400, Priceof50 = 7000, priceof100 = 85000, CategoryId = 3, ImgUrl = "" }
                );

        }
    }
}
