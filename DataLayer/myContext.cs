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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().ToTable("Pizzas");
            base.OnModelCreating(modelBuilder);
        }
    }
}
