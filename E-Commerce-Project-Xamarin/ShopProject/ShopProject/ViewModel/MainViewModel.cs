using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Windows.Input;

namespace ShopProject.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            LoadProducts();
            MenuList = GetMenus();
            MenuCommand = new Command<Menu>(OnMenuClicked);
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Menu> menuList;
        public ObservableCollection<Menu> MenuList
        {
            get { return menuList; }
            set
            {
                menuList = value;
                OnPropertyChanged();
            }
        }

        public ICommand MenuCommand { get; set; }

        public async void LoadProducts()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetStringAsync("http://192.168.100.220:8080/api/produits/afficheTous");
                var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(response);
                Products = products;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void ShowDetails()
        {
            if (SelectedProduct != null)
            {
                var page = new DetailsPage(SelectedProduct);
                App.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                // Handle the case where no product is selected
                Console.WriteLine("No product selected");
            }
        }

        private void OnMenuClicked(Menu menu)
        {
            if (menu.Name == "Shopping Cart")
            {
                // Navigate to CartPage
                App.Current.MainPage.Navigation.PushAsync(new CartPage(LoginPage.LoggedInUserId));
            }
            else if (menu.Name == "Settings")
            {
                // Log out the user
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            else if (menu.Name == "My Order")
            {
                // Navigate to OrderPage
                App.Current.MainPage.Navigation.PushAsync(new OrderPage());
            }

        }

        private ObservableCollection<Menu> GetMenus()
        {
            return new ObservableCollection<Menu>
            {
                new Menu { Icon = "order.png", Name = "My Order" },
                new Menu { Icon = "shopping.png", Name = "Shopping Cart" },
                new Menu { Icon = "settings.png", Name = "Log Out" }
            };
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

 

    public class Menu
    {
        public string Name { get; set; }
        public string Icon { get; set; }
    }
}