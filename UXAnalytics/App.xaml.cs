using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Xamarin.Forms;

namespace UXAnalytics
{
    public partial class App : Application
    {
        private string iOSAppCenterKey = "{your-appcenter-key}";
        private string AndroidAppCenterKey = "{your-appcenter-key}";

        public App()
        {
            InitializeComponent();
            RegisterPagesForAnalytic();

            MainPage = new MainPage();
        }

        private void RegisterPagesForAnalytic()
        {
            Logger.RegisterPage(typeof(MainPageViewModel), "Main page");
        }

        protected override void OnStart()
        {
            AppCenter.Start($"ios={iOSAppCenterKey};" +
                  $"android={AndroidAppCenterKey}",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
