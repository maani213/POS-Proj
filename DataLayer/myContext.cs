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

        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<Extras> Extras { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sizes>().ToTable("Sizes");
            modelBuilder.Entity<Item>().ToTable("Items");
            modelBuilder.Entity<Extras>().ToTable("Extras");
            modelBuilder.Entity<Categories>().ToTable("Categories");
            modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");
            modelBuilder.Entity<Orders>().ToTable("Order");
            base.OnModelCreating(modelBuilder);
        }
    }
}
