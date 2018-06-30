using Raktar.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;


namespace Raktar.DAL
{
    public class RaktarContext2 : DbContext
    {

        public RaktarContext2() : base("RaktarContext2")
        {
        }

        public DbSet<Renter> Renter { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<Loan> Loan { get; set; }
        public DbSet<Stock> Stock { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Loan>()
                .HasRequired(a => a.Renter)
                .WithMany()
                .HasForeignKey(u => u.RenterID);

            modelBuilder.Entity<Loan>()
                .HasRequired(a => a.Equipment)
                .WithMany()
                .HasForeignKey(u => u.EquipmentID);

            modelBuilder.Entity<Stock>()
                .HasRequired(a => a.Equipment)
                .WithMany()
                .HasForeignKey(u => u.EquipmentID);

            //modelBuilder.Entity<Stock>().MapToStoredProcedures();
        }
    }
}