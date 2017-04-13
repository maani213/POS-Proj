using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class myContext:DbContext
    {
        public myContext(): base("POSdb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<myContext, DataLayer.Migrations.Configuration>("POSdb"));
        }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().ToTable("Pizzas");
            modelBuilder.Entity<OrderDetails>().ToTable("OrderDetails");
            modelBuilder.Entity<Orders>().ToTable("Order");
            base.OnModelCreating(modelBuilder);
        }
    }
}
