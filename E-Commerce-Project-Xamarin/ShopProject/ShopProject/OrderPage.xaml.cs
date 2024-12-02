using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ShopProject.ViewModel;

namespace ShopProject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderPage : ContentPage
    {
        public OrderPage()
        {
            InitializeComponent();
            BindingContext = new OrderViewModel();
        }
    }
}