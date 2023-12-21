using MyDeliveryApp.MVVM.Models;
using MyDeliveryApp.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MyDeliveryApp.MVVM.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var mainWindowViewModel = new MainWindowViewModel();
            DataContext = mainWindowViewModel;

            ParcelTab.DataContext = mainWindowViewModel.ParcelViewModel;
            ClientsTab.DataContext = mainWindowViewModel.ClientsViewModel;
        }
        private void ParcelsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {

                var selectedParcel = dataGrid.SelectedItem as Parcel;


                (DataContext as MainWindowViewModel)?.ParcelViewModel.SetSelectedParcel(selectedParcel);
            }
        }
        private void ClientsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is DataGrid dataGrid)
            {

                var selectedClient = dataGrid.SelectedItem as Client;

                (DataContext as MainWindowViewModel)?.ClientsViewModel.SetSelectedClient(selectedClient);
            }
        }
    }
}