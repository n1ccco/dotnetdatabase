namespace MyDeliveryApp.MVVM.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public List<Parcel> Parcels { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}