using Microsoft.EntityFrameworkCore;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.Data
{
    public class UltimateDBContext :DbContext
    {
        public UltimateDBContext(DbContextOptions<UltimateDBContext> options) :base(options) { }


        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Categories and Products
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories)      // Each product has one category
                .WithMany(c => c.Products)      // Each category can have many products
                .HasForeignKey(p => p.CategoryID);  // Foreign key is CategoryID

            // Set up any additional configuration if needed

            base.OnModelCreating(modelBuilder);
        }


    }
}
