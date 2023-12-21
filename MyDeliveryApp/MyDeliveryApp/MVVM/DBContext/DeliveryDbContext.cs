using Microsoft.EntityFrameworkCore;
using MyDeliveryApp.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDeliveryApp.MVVM.DBContext
{
    public class DeliveryDbContext : DbContext
    {
        public DbSet<Parcel> Parcels { get; set; }
        public DbSet<Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Parcel>()
                .HasOne(p => p.Client)
                .WithMany(c => c.Parcels)
                .HasForeignKey(p => p.RecipientID);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string dbPath = "Data Source=../../Database/delivery.sqlite";
            string dbPath = "Data Source=C:/Users/Nico/source/repos/MyDeliveryApp/MyDeliveryApp/Database/delivery.sqlite";
            optionsBuilder.UseSqlite(dbPath);
        }

    }
}
