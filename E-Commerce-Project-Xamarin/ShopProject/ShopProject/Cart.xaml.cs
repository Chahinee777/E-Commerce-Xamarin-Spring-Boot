using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShopProject.ViewModel;

namespace ShopProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            BindingContext = new CartViewModel(LoginPage.LoggedInUserId);
        }

        public CartPage(long userId)
        {
            InitializeComponent();
            BindingContext = new CartViewModel(userId);
        }
    }
}