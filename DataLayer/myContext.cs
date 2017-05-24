using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class myContext : DbContext
    {
        public myContext() : base("POSdb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<myContext, DataLayer.Migrations.Configuration>("POSdb"));

        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<ExtrasAndTopping> ExtrasAndToppings { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<CookingInstruction> CookingInstructions { get; set; }
        public DbSet<AwaitOrder> AwaitOrders { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Size>().ToTable("Size");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<ExtrasAndTopping>().ToTable("ExtrasAndTopping");
            modelBuilder.Entity<Category>().ToTable("Category");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<PaymentType>().ToTable("PaymentType");
            modelBuilder.Entity<Discount>().ToTable("Discount");
            modelBuilder.Entity<CookingInstruction>().ToTable("CookingInstruction");

            modelBuilder.Entity<AwaitOrder>().ToTable("AwaitOrder");
            base.OnModelCreating(modelBuilder);
        }
    }
}
