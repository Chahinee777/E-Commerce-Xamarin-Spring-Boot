using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ShopProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminLoginPage : ContentPage
    {
        public AdminLoginPage()
        {
            InitializeComponent();
        }

        private async void AdminLoginBtn_Clicked(object sender, EventArgs e)
        {
            string email = AdminEmail.Text;
            string password = AdminPassword.Text;

            if (email == "admin@admin.com" && password == "admin123")
            {
                await DisplayAlert("Welcome Admin", "You are logged in as admin", "OK");
                await Navigation.PushAsync(new AdminPage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid admin credentials", "OK");
            }
        }
    }
}