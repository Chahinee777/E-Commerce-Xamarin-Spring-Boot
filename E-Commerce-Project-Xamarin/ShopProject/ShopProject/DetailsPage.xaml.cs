using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShopProject.ViewModel;

namespace ShopProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public DetailsPage(Product selectedProduct)
        {
            InitializeComponent();
            BindingContext = new DetailsViewModel { SelectedProduct = selectedProduct };
        }

        private async void AddToCartClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as DetailsViewModel;
            if (viewModel == null)
            {
                await DisplayAlert("Error", "ViewModel is not set", "OK");
                return;
            }

            var selectedProduct = viewModel.SelectedProduct;
            if (selectedProduct == null)
            {
                await DisplayAlert("Error", "No product selected", "OK");
                return;
            }

            bool success = await AddToCart(selectedProduct.Id, LoginPage.LoggedInUserId);
            if (success)
            {
                await DisplayAlert("Success", "Product added to cart", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Failed to add product to cart", "OK");
            }
        }

        private async Task<bool> AddToCart(long productId, long userId)
        {
            try
            {
                var client = new HttpClient();
                var response = await client.PostAsync($"http://192.168.100.220:8080/api/users/addCart?productId={productId}&userId={userId}", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        private void BackTapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
    }