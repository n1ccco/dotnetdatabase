using Microsoft.Extensions.DependencyInjection;
using MyDeliveryApp.MVVM.DBContext;
using MyDeliveryApp.MVVM.Seeders;
using MyDeliveryApp.MVVM.Views;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MyDeliveryApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var dbContext = new DeliveryDbContext();

            var serviceProvider = new ServiceCollection()
                .AddDbContext<DeliveryDbContext>()
                .AddTransient<ISeeder, DatabaseSeeder>()
                .BuildServiceProvider();


            using (var scope = serviceProvider.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
                //seeder.Seed(dbContext);
            }

            InitializeApp();
        }

        private void InitializeApp()
        {

            MainWindow mainWindow = new MainWindow();

            mainWindow.Show();
        }
    }

}
