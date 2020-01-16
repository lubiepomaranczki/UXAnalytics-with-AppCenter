using System.ComponentModel;
using Xamarin.Forms;

namespace UXAnalytics
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPageViewModel ViewModel => BindingContext as MainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel?.OnViewAppearing();            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ViewModel?.OnViewDisappearing();
        }
    }
}
