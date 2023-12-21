using System;
using System.Windows.Controls;

namespace MyDeliveryApp.MVVM.Models
{
    public class Parcel
    {
        public int ParcelID { get; set; }
        public int RecipientID { get; set; }
        public Client Client { get; set; } 
        public string Origin { get; set; }  
        public string Destination { get; set; }
        public double Weight { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}
