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
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<TransactionProduct> TransactionProducts { get; set; }
        public DbSet<Payments> PaymentDetails { get; set; }


//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-K2BT32U;Database=MinimartDB;User Id=Caleb;Password=Caleb@2543");
//            }
//        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring the many-to-many relationship
            modelBuilder.Entity<TransactionProduct>()
                .HasOne(tp => tp.Transaction)
                .WithMany(t => t.TransactionProducts)
                .HasForeignKey(tp => tp.TransactionID);


            modelBuilder.Entity<TransactionProduct>()
                    .HasOne(tp => tp.Product)
                    .WithMany()
                    .HasForeignKey(tp => tp.ProductID);

            // One-to-many relationship between Transactions and PaymentDetails
            modelBuilder.Entity<Transactions>()
                .HasOne(t => t.PaymentDetails)
                .WithMany(pd => pd.Transactions)
                .HasForeignKey(t => t.PaymentMethodID);
        


            // Configure the one-to-many relationship between Categories and Products
            modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories)      // Each product has one category
                .WithMany(c => c.Products)      // Each category can have many products
                .HasForeignKey(p => p.CategoryID);  // Foreign key is CategoryID

            // Set up any additional configuration if needed
            modelBuilder.Entity<Products>()
            .Property(p => p.BuyingPrice)
            .HasColumnType("decimal(18, 2)");

            modelBuilder.Entity<Products>()
                .Property(p => p.SellingPrice)
                .HasColumnType("decimal(18, 2)");


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
