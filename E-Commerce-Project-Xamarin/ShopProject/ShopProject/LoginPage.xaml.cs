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
    public partial class LoginPage : ContentPage
    {
        public static long LoggedInUserId { get; private set; }

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void LoginBtn_Clicked(object sender, EventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var user = new
                {
                    email = UserEmail.Text,
                    password = UserPassword.Text
                };
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://192.168.100.220:8080/api/users/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response status code: {response.StatusCode}");
                Console.WriteLine($"Response content: {responseContent}");
                if (response.IsSuccessStatusCode)
                {
                    var loggedInUser = JsonConvert.DeserializeObject<User>(responseContent);
                    LoggedInUserId = loggedInUser.Id; // Store the logged-in user ID
                    await DisplayAlert("Welcome", "You are logged in", "OK");
                    await Navigation.PushAsync(new MainPage());
                }
                else
                {
                    Console.WriteLine($"Login failed: {responseContent}"); // Log the error message
                    await DisplayAlert("Error", $"Login failed: {responseContent}", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}"); // Log the exception
                await DisplayAlert("Failed", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private void RegisterRedirect_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void AdminRedirect_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AdminLoginPage());
        }
    }

    public class User
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}