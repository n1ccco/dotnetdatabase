using MyDeliveryApp.Core;
using MyDeliveryApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MyDeliveryApp.MVVM.DBContext;

namespace MyDeliveryApp.MVVM.ViewModels
{
    public class ParcelViewModel : ObservableObject
    {
        private ObservableCollection<Parcel> _parcels;
        private Parcel _selectedParcel;

        private string _recipientIdText;
        private string _origin;
        private string _destination;
        private string _weightText;
        private string _sizeText;
        private string _statusText;
        private DateTime _deliveryDate;

        public ObservableCollection<Parcel> Parcels
        {
            get { return _parcels; }
            set { SetProperty(ref _parcels, value); }
        }
        public void SetSelectedParcel(Parcel parcel)
        {
            SelectedParcel = parcel;

            RecipientIdText = SelectedParcel?.RecipientID.ToString();
            WeightText = SelectedParcel?.Weight.ToString();
            OriginText = SelectedParcel?.Origin;
            DestinationText = SelectedParcel?.Destination;
            SizeText = SelectedParcel?.Size;
            StatusText = SelectedParcel?.Status;
            DeliveryDateText = SelectedParcel?.DeliveryDate ?? DateTime.Now;
        }

        public Parcel SelectedParcel
        {
            get { return _selectedParcel; }
            set { SetProperty(ref _selectedParcel, value); }
        }

        public RelayCommand EditParcelCommand { get; set; }
        public RelayCommand DeleteParcelCommand { get; set; }
        public RelayCommand AddParcelCommand { get; set; }

        public ParcelViewModel()
        {
            LoadParcels();
            InitializeCommands();
        }

        private void LoadParcels()
        {
            using (var dbContext = new DeliveryDbContext())
            {
                var parcels = from parcel in dbContext.Parcels
                              select parcel;

                Parcels = new ObservableCollection<Parcel>(parcels.ToList());
            }
        }

        private void InitializeCommands()
        {
            EditParcelCommand = new RelayCommand(EditParcel, CanEditParcel);
            DeleteParcelCommand = new RelayCommand(DeleteParcel, CanDeleteParcel);
            AddParcelCommand = new RelayCommand(AddParcel);
        }

        private void EditParcel(object parameter)
        {
            using (var dbContext = new DeliveryDbContext())
            {
                var originalParcel = dbContext.Parcels.Find(SelectedParcel.ParcelID);

                if (originalParcel != null)
                {
                    originalParcel.RecipientID = int.Parse(RecipientIdText);
                    originalParcel.Weight = double.Parse(WeightText);
                    originalParcel.Origin = OriginText;
                    originalParcel.Destination = DestinationText;
                    originalParcel.Size = SizeText;
                    originalParcel.Status = StatusText;
                    originalParcel.DeliveryDate = DeliveryDateText;

                    dbContext.SaveChanges();
                    LoadParcels();
                }
            }
        }

        private bool CanEditParcel(object parameter)
        {
            return SelectedParcel != null;
        }
        private void DeleteParcel(object parameter)
        {
            using (var dbContext = new DeliveryDbContext())
            {
                var parcelToDelete = dbContext.Parcels.FirstOrDefault(p => p.ParcelID == SelectedParcel.ParcelID);

                if (parcelToDelete != null)
                {
                    dbContext.Parcels.Remove(parcelToDelete);
                    dbContext.SaveChanges();

                    LoadParcels();
                }
            }
        }
        private bool CanDeleteParcel(object parameter)
        {
            return SelectedParcel != null;
        }

        public string RecipientIdText
        {
            get { return _recipientIdText; }
            set
            {
                if (_recipientIdText != value && value != null)
                {
                    _recipientIdText = value;
                    OnPropertyChanged(nameof(RecipientIdText));
                }
            }
        }

        public string OriginText
        {
            get { return _origin; }
            set
            {
                if (_origin != value)
                {
                    _origin = value;
                    OnPropertyChanged(nameof(OriginText));
                }
            }
        }

        public string DestinationText
        {
            get { return _destination; }
            set
            {
                if (_destination != value)
                {
                    _destination = value;
                    OnPropertyChanged(nameof(DestinationText));
                }
            }
        }

        public string WeightText
        {
            get { return _weightText; }
            set
            {
                if (_weightText != value)
                {
                    _weightText = value;
                    OnPropertyChanged(nameof(WeightText));
                }
            }
        }

        public string SizeText
        {
            get { return _sizeText; }
            set
            {
                if (_sizeText != value)
                {
                    _sizeText = value;
                    OnPropertyChanged(nameof(SizeText));
                }
            }
        }

        public string StatusText
        {
            get { return _statusText; }
            set
            {
                if (_statusText != value)
                {
                    _statusText = value;
                    OnPropertyChanged(nameof(StatusText));
                }
            }
        }

        public DateTime DeliveryDateText
        {
            get { return _deliveryDate; }
            set
            {
                if (_deliveryDate != value)
                {
                    _deliveryDate = value;
                    OnPropertyChanged(nameof(DeliveryDateText));
                }
            }
        }
        private void AddParcel(object parameter)
        {
            if (int.TryParse(RecipientIdText, out int recipientId) &&
        double.TryParse(WeightText, out double weight))
            {

                var newParcel = new Parcel
                {
                    RecipientID = recipientId,
                    Weight = weight,
                    Origin = OriginText,
                    Destination = DestinationText,
                    Size = SizeText,
                    Status = StatusText,
                    DeliveryDate = DeliveryDateText
                };



                using (var dbContext = new DeliveryDbContext())
                {
                    dbContext.Parcels.Add(newParcel);
                    dbContext.SaveChanges();
                }

                Parcels.Add(newParcel);
                SelectedParcel = newParcel;


                RecipientIdText = "";
                WeightText = "";
                OriginText = "";
                DestinationText = "";
                SizeText = "";
                StatusText = "";
                DeliveryDateText = DateTime.Now;
            }
        }
    }
}