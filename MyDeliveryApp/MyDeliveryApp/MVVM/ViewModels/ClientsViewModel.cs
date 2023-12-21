using MyDeliveryApp.Core;
using MyDeliveryApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MyDeliveryApp.MVVM.DBContext;

namespace MyDeliveryApp.MVVM.ViewModels
{
    public class ClientsViewModel : ObservableObject
    {
        private ObservableCollection<Client> _clients;
        private Client _selectedClient;

        private string _firstNameText;
        private string _lastNameText;
        private string _emailText;
        private string _phoneNumberText;
        private string _addressText;

        public ObservableCollection<Client> Clients
        {
            get { return _clients; }
            set { SetProperty(ref _clients, value); }
        }
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                SetProperty(ref _selectedClient, value);
                UpdateClientFields();
            }
        }

        public void SetSelectedClient(Client client)
        {
            SelectedClient = client;
        }

        private void UpdateClientFields()
        {
            if (SelectedClient != null)
            {
                FirstNameText = SelectedClient.FirstName;
                LastNameText = SelectedClient.LastName;
                EmailText = SelectedClient.Email;
                PhoneNumberText = SelectedClient.PhoneNumber;
                AddressText = SelectedClient.Address;
            }
            else
            {
                FirstNameText = "";
                LastNameText = "";
                EmailText = "";
                PhoneNumberText = "";
                AddressText = "";
            }
        }


        public RelayCommand EditClientCommand { get; set; }
        public RelayCommand DeleteClientCommand { get; set; }
        public RelayCommand AddClientCommand { get; set; }

        public ClientsViewModel()
        {
            LoadClients();
            InitializeCommands();
        }

        private void LoadClients()
        {
            using (var dbContext = new DeliveryDbContext())
            {
                var clients = from client in dbContext.Clients
                              select client;

                Clients = new ObservableCollection<Client>(clients.ToList());
            }
        }

        private void InitializeCommands()
        {
            EditClientCommand = new RelayCommand(EditClient, CanEditClient);
            DeleteClientCommand = new RelayCommand(DeleteClient, CanDeleteClient);
            AddClientCommand = new RelayCommand(AddClient);
        }

        private void EditClient(object parameter)
        {
            using (var dbContext = new DeliveryDbContext())
            {
                var originalClient = dbContext.Clients.Find(SelectedClient.ClientID);

                if (originalClient != null)
                {
                    originalClient.FirstName = FirstNameText;
                    originalClient.LastName = LastNameText;
                    originalClient.Email = EmailText;
                    originalClient.PhoneNumber = PhoneNumberText;
                    originalClient.Address = AddressText;

                    dbContext.SaveChanges();
                    LoadClients();
                }
            }
        }

        private bool CanEditClient(object parameter)
        {
            return SelectedClient != null;
        }

        private void DeleteClient(object parameter)
        {
            using (var dbContext = new DeliveryDbContext())
            {
                var clientToDelete = dbContext.Clients.FirstOrDefault(c => c.ClientID == SelectedClient.ClientID);

                if (clientToDelete != null)
                {
                    dbContext.Clients.Remove(clientToDelete);
                    dbContext.SaveChanges();

                    LoadClients();
                }
            }
        }

        private bool CanDeleteClient(object parameter)
        {
            return SelectedClient != null;
        }

        public string FirstNameText
        {
            get { return _firstNameText; }
            set
            {
                if (_firstNameText != value)
                {
                    _firstNameText = value;
                    OnPropertyChanged(nameof(FirstNameText));
                }
            }
        }

        public string LastNameText
        {
            get { return _lastNameText; }
            set
            {
                if (_lastNameText != value)
                {
                    _lastNameText = value;
                    OnPropertyChanged(nameof(LastNameText));
                }
            }
        }

        public string EmailText
        {
            get { return _emailText; }
            set
            {
                if (_emailText != value)
                {
                    _emailText = value;
                    OnPropertyChanged(nameof(EmailText));
                }
            }
        }

        public string PhoneNumberText
        {
            get { return _phoneNumberText; }
            set
            {
                if (_phoneNumberText != value)
                {
                    _phoneNumberText = value;
                    OnPropertyChanged(nameof(PhoneNumberText));
                }
            }
        }

        public string AddressText
        {
            get { return _addressText; }
            set
            {
                if (_addressText != value)
                {
                    _addressText = value;
                    OnPropertyChanged(nameof(AddressText));
                }
            }
        }

        private void AddClient(object parameter)
        {

            var newClient = new Client
            {
                FirstName = FirstNameText,
                LastName = LastNameText,
                Email = EmailText,
                PhoneNumber = PhoneNumberText,
                Address = AddressText
            };

            using (var dbContext = new DeliveryDbContext())
            {
                dbContext.Clients.Add(newClient);
                dbContext.SaveChanges();
            }

  
            LoadClients();


            FirstNameText = "";
            LastNameText = "";
            EmailText = "";
            PhoneNumberText = "";
            AddressText = "";


            SelectedClient = newClient;
        }
    }
}