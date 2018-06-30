namespace Raktar.Migrations
{
    using Raktar.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Raktar.DAL.RaktarContext2>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //ContextKey = "Raktar.DAL.RaktarContext2";
        }

        protected override void Seed(Raktar.DAL.RaktarContext2 context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var renters = new List<Renter>
            {
                new Renter{RenterID="F1234",Name="Almási Béla", City="Miskolc", ZipCode=3200, StreetAddress="Kussuth Lajos  11.", Mail= "jhgdf@freemail.hu", Phone= 0036208884466},
                new Renter{RenterID="F4321",Name="Kossuth Lajos", City="Budapest", ZipCode=0254, StreetAddress="Mátyás 15.", Mail= "jhgdf@freemail.hu", Phone= 0036705467852}

            };
            renters.ForEach(s => context.Renter.AddOrUpdate(s));
            context.SaveChanges();



            var equipments = new List<Equipment>
            {
                new Equipment{Describe="siléc+kötés", EquipmentID="s1111", Brand="Atomic", Type="Cloud 7 LT", AdditionalBrand = "Atomic", AdditionalType= "XTL 9 L",
                AdditionalDescribe="Kötés", NettoPrice=55000, BruttoPrice=68750, },
                new Equipment{Describe="snowboard+kötés", EquipmentID="s2222", Brand="Atomic", Type="Vantage", AdditionalBrand = "Atomic", AdditionalType= "Straight Shot",
                AdditionalDescribe="Kötés", NettoPrice=63000, BruttoPrice=78750,}
            };


            equipments.ForEach(s => context.Equipment.AddOrUpdate(s));
            context.SaveChanges();

            var loans = new List<Loan>
            {
                new Loan{LoanID="r3333", RenterID="F1234", EquipmentID="s1111", Quantity=1, Date=DateTime.Parse("2017-09-01")},
                new Loan{LoanID="r4444", RenterID="F1234", EquipmentID="s2222", Quantity=2, Date=DateTime.Parse("2017-06-01")},
                new Loan{LoanID="r5555", RenterID="F4321", EquipmentID="s1111", Quantity=4, Date=DateTime.Parse("2017-12-21")},
            };

            loans.ForEach(s => context.Loan.AddOrUpdate(s));
            context.SaveChanges();

            var stocks = new List<Stock>
            {
                new Stock{StockID="b8888", EquipmentID="s1111", InStock= true, SQuantity=5 },
                new Stock{StockID="b9999", EquipmentID="s2222", InStock= true, SQuantity=4 },
            };

            stocks.ForEach(s => context.Stock.AddOrUpdate(s));
            context.SaveChanges();
        }
    }
}
