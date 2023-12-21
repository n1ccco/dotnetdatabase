using Microsoft.EntityFrameworkCore;
using MyDeliveryApp.MVVM.DBContext;
using MyDeliveryApp.MVVM.Models;
using System;
using System.Linq;

namespace MyDeliveryApp.MVVM.Seeders
{
    public class DatabaseSeeder : ISeeder
    {
        public void Seed(DeliveryDbContext context)
        {

            context.Database.Migrate();
            if (!context.Clients.Any())
            {
                var clients = new[]
                {
                    new Client { FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", PhoneNumber = "123-456-7890", Address = "123 Main St" },
                    new Client { FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", PhoneNumber = "987-654-3210", Address = "456 Oak St" },
                };

                context.Clients.AddRange(clients);
                context.SaveChanges();
            }

            if (!context.Parcels.Any())
            {
                var parcels = new[]
                {
                    new Parcel { RecipientID = 1, Origin = "City A", Destination = "City B", Weight = 2.5, Size = "Small", Status = "In Transit", DeliveryDate = DateTime.Now.AddDays(3) },
                    new Parcel { RecipientID = 2, Origin = "City C", Destination = "City D", Weight = 1.8, Size = "Medium", Status = "Delivered", DeliveryDate = DateTime.Now.AddDays(-1) },

                };

                context.Parcels.AddRange(parcels);
                context.SaveChanges();
            }
        }
    }
}