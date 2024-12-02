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
    public class CartViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<CartItemViewModel> CartItems { get; set; }
        private long userId;
        private readonly HttpClient client;

        public CartViewModel()
        {
            CartItems = new ObservableCollection<CartItemViewModel>();
            EmptyCartCommand = new Command(EmptyCart);
            IncreaseQuantityCommand = new Command<CartItemViewModel>(IncreaseQuantity);
            DecreaseQuantityCommand = new Command<CartItemViewModel>(DecreaseQuantity);
            CheckoutCommand = new Command(Checkout);
            client = new HttpClient();
        }

        public CartViewModel(long userId) : this()
        {
            this.userId = userId;
            LoadCartItems();
        }

        private async void LoadCartItems()
        {
            try
            {
                var response = await client.GetStringAsync($"http://192.168.100.220:8080/api/users/{userId}/cart");
                var cartItems = JsonConvert.DeserializeObject<List<CartItem>>(response);
                CartItems.Clear();
                foreach (var item in cartItems)
                {
                    CartItems.Add(new CartItemViewModel
                    {
                        Id = item.Id,
                        Name = item.Product.Name,
                        Quantity = item.Quantite,
                        TotalPrice = item.PrixTotal,
                        Price = item.Product.Price
                    });
                }
                OnPropertyChanged(nameof(CartItems));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public ICommand EmptyCartCommand { get; }
        public ICommand IncreaseQuantityCommand { get; }
        public ICommand DecreaseQuantityCommand { get; }
        public ICommand CheckoutCommand { get; }

        private void EmptyCart()
        {
            CartItems.Clear();
            // Add logic to clear the cart in the backend if needed
        }

        private async void IncreaseQuantity(CartItemViewModel item)
        {
            item.Quantity++;
            item.TotalPrice = item.Quantity * item.Price;
            await UpdateCartQuantity("in", item.Id);
        }

        private async void DecreaseQuantity(CartItemViewModel item)
        {
            if (item.Quantity > 1)
            {
                item.Quantity--;
                item.TotalPrice = item.Quantity * item.Price;
                await UpdateCartQuantity("de", item.Id);
            }
        }

        private async Task UpdateCartQuantity(string action, long cartId)
        {
            try
            {
                var response = await client.GetStringAsync($"http://192.168.100.220:8080/api/users/updateCartQuantity?sy={action}&cartId={cartId}");
                Console.WriteLine($"Cart quantity updated: {response}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private async void Checkout()
        {
            try
            {
                var response = await client.PostAsync($"http://192.168.100.220:8080/api/users/save-order?userId={userId}", null);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Order saved successfully");
                    CartItems.Clear();
                    OnPropertyChanged(nameof(CartItems));
                    // Display success message
                    await Application.Current.MainPage.DisplayAlert("Success", "Order successfully", "OK");
                }
                else
                {
                    Console.WriteLine($"Checkout failed: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during checkout: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CartItemViewModel : INotifyPropertyChanged
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int TotalPrice { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class CartItem
    {
        public long Id { get; set; }
        public Product Product { get; set; }
        public int Quantite { get; set; }
        public int PrixTotal { get; set; }
    }

    
}