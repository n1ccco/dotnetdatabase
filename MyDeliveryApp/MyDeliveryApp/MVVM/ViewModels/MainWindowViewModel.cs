using MyDeliveryApp.Core;
using MyDeliveryApp.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using MyDeliveryApp.MVVM.DBContext;

namespace MyDeliveryApp.MVVM.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private ParcelViewModel _parcelViewModel;
        private ClientsViewModel _clientsViewModel;

        public ParcelViewModel ParcelViewModel
        {
            get { return _parcelViewModel; }
            set { SetProperty(ref _parcelViewModel, value); }
        }

        public ClientsViewModel ClientsViewModel
        {
            get { return _clientsViewModel; }
            set { SetProperty(ref _clientsViewModel, value); }
        }

        public MainWindowViewModel()
        {

            ParcelViewModel = new ParcelViewModel();
            ClientsViewModel = new ClientsViewModel();

        }
    }
}