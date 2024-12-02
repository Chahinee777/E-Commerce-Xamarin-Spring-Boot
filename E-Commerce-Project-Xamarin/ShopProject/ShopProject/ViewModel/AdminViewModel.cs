using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ShopProject.ViewModel
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient client;

        public ObservableCollection<Order> Orders { get; set; }

        public ICommand LoadOrdersCommand { get; }
        public ICommand DenyOrdersCommand { get; }

        public AdminViewModel()
        {
            Orders = new ObservableCollection<Order>();
            client = new HttpClient();

            LoadOrdersCommand = new Command(LoadOrders);
            DenyOrdersCommand = new Command(DenyOrders);

            LoadOrders();
        }

        private async void LoadOrders()
        {
            try
            {
                var response = await client.GetStringAsync("http://192.168.100.220:8080/api/orders/afficheTous");
                var orders = JsonConvert.DeserializeObject<ObservableCollection<Order>>(response);
                Orders.Clear();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void DenyOrders()
        {
            try
            {
                var response = await client.DeleteAsync("http://192.168.100.220:8080/api/orders/deleteAll");
                if (response.IsSuccessStatusCode)
                {
                    Orders.Clear();
                }
                else
                {
                    Console.WriteLine("Failed to delete orders.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}