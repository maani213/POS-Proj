namespace DataLayer.Migrations
{
    using Entities;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.myContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;

        }

        //protected override void Seed(DataLayer.myContext context)
        //{
        //    Item item = new Item();
        //    item.BackgroundColor = "black";
        //    item.FontSize = 15;
        //    item.TextColor = "white";
        //    item.TextStyle = "verdana";
        //    item.Title = "Test";
        //    item.IsBold = true;
        //    item.IsItalic = true;
        //    item.Toppings = "Test";
        //    item.Price1 = 1;
        //    item.Price2 = 2;
        //    item.Price3 = 3;
        //    item.CategoryId = 1;

        //    Category category = new Category()
        //    {
        //        Title = "Pizza",
        //        BackgroundColor = "green",
        //        TextColor = "white",
        //    };

        //    context.Categories.AddOrUpdate(category);
        //    context.Items.AddOrUpdate(item);
        //}
    }
}
