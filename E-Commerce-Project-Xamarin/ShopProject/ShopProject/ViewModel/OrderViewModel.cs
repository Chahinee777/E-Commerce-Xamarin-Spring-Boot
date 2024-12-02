using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ShopProject.ViewModel
{
    public class OrderViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Order> Orders { get; set; }
        private readonly HttpClient client;

        public ICommand ContinueShoppingCommand { get; }

        public OrderViewModel()
        {
            Orders = new ObservableCollection<Order>();
            ContinueShoppingCommand = new Command(ContinueShopping);
            client = new HttpClient();
            LoadOrders();
        }

        private async void LoadOrders()
        {
            try
            {
                var response = await client.GetStringAsync($"http://192.168.100.220:8080/api/orders/user/{LoginPage.LoggedInUserId}");
                var orders = JsonConvert.DeserializeObject<List<Order>>(response);
                Orders.Clear();
                foreach (var order in orders)
                {
                    Orders.Add(order);
                }
                OnPropertyChanged(nameof(Orders));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void ContinueShopping()
        {
            // Navigate back to the main page or any other page
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}