using MyDeliveryApp.MVVM.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDeliveryApp.MVVM.Seeders
{
    public interface ISeeder
    {
        void Seed(DeliveryDbContext context);
    }
}
