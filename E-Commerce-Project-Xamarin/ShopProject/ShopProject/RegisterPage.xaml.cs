using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void RegisterBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (UserPassword.Text == UserConfirmPassword.Text)
                {
                    var client = new HttpClient();
                    var user = new
                    {
                        email = UserEmail.Text,
                        password = UserPassword.Text
                    };
                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("http://192.168.100.220:8080/api/users/register", content);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Response status code: {response.StatusCode}");
                    Console.WriteLine($"Response content: {responseContent}");

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Welcome", "You are registered", "OK");
                        await Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        Console.WriteLine($"Registration failed: {responseContent}"); // Log the error message
                        await DisplayAlert("Error", $"Registration failed: {responseContent}", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "Passwords do not match", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}"); // Log the exception
                await DisplayAlert("Failed", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void alreadyHaveAccount_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}