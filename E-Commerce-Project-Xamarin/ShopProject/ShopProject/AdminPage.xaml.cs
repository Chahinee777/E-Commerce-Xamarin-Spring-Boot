using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShopProject.ViewModel;

namespace ShopProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminPage : ContentPage
    {
        public AdminPage()
        {
            InitializeComponent();
            BindingContext = new AdminViewModel();
        }
    }
}