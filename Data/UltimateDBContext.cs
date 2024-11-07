using Microsoft.EntityFrameworkCore;
using Ultimate_POS_Api.DTOS;
using Ultimate_POS_Api.Models;

namespace Ultimate_POS_Api.Data
{
    public class UltimateDBContext : DbContext
    {
        public UltimateDBContext(DbContextOptions<UltimateDBContext> options) : base(options) { }


        public DbSet<Products> Products { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PaymentDetails> PaymentDetails { get; set; }
        public DbSet<Payments> Payments { get; set; }

        public DbSet<Transactions> Transactions { get; set; }

        public DbSet<Supplier> Supplier { get; set; } 

          public DbSet<Supplies> Supplies { get; set; }

        public DbSet<TransactionProduct> TransactionProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the one-to-many relationship between Categories and Products
            _ = modelBuilder.Entity<Products>()
                .HasOne(p => p.Categories)      // Each product has one category
                .WithMany(c => c.Products)      // Each category can have many products
                .HasForeignKey(p => p.CategoryID);  // Foreign key is CategoryID

            // Set up any additional configuration if needed

            _ = modelBuilder.Entity<UserRole>()
                .HasKey(ur => ur.UserRoleId); // proimary key for UserRole 


            //create relatiooship for User
            _ = modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User) //each userrole can have on userID
                .WithMany(u => u.UserRoles) //one user can have many Userroles
                .HasForeignKey(ur => ur.UserId);

            //create relatiooship for Role
            _ = modelBuilder.Entity<UserRole>()
              .HasOne(ur => ur.Role) //each userrole can have on Role
              .WithMany(r => r.UserRoles) //one role can have many Userroles
              .HasForeignKey(ur => ur.RoleId);

            _ = modelBuilder.Entity<Supplier>()
           .HasKey(ur => ur.SupplierId);

            // _ = modelBuilder.Entity<Supplier>()
            //  .HasOne(us => us.PaymentDetails);      // Each suplier can have only one payment details

            // _ = modelBuilder.Entity<PaymentDetail>()
            //  .HasKey(up => up.Id);

            // _ = modelBuilder.Entity<PaymentDetail>()
            //  .HasOne(pd => pd.bankAccount); //each payment mode can only contain one bank account

            // _ = modelBuilder.Entity<PaymentDetail>()
            //  .HasOne(up => up.mpesa); //each payment mode can only contain one bank account

            // _ = modelBuilder.Entity<BankAccount>().HasNoKey();

            // _ = modelBuilder.Entity<Mpesa>().HasNoKey();

            // _ = modelBuilder.Entity<Paybill>().HasNoKey();


            //         modelBuilder.Entity<PaymentDetail>()
            // .       Ignore(p => p.Id);


            //         modelBuilder.Entity<PaymentDetail>()
            // .         Ignore(k => k.BankDetail);

            //        modelBuilder.Entity<mpesa>()
            // .         Ignore(g => g.Paybill);


            base.OnModelCreating(modelBuilder);
        }

    }
}
