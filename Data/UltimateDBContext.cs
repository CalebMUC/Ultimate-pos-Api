using Microsoft.EntityFrameworkCore;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.Data
{
    public class UltimateDBContext :DbContext
    {
        public UltimateDBContext(DbContextOptions<UltimateDBContext> options) :base(options) { }


        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Categories and Products
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories)      // Each product has one category
                .WithMany(c => c.Products)      // Each category can have many products
                .HasForeignKey(p => p.CategoryID);  // Foreign key is CategoryID

            // Set up any additional configuration if needed

            modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.UserRoleId); // proimary key for UserRole 

            //create relatiooship for User
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User) //each userrole can have on userID
                .WithMany(u => u.UserRoles) //one user can have many Userroles
                .HasForeignKey(ur => ur.UserId);

                  //create relatiooship for Role
            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role) //each userrole can have on Role
                .WithMany(r => r.UserRoles) //one role can have many Userroles
                .HasForeignKey(ur => ur.RoleId);




            base.OnModelCreating(modelBuilder);
        }


    }
}
