using XamForms.Enhanced.ViewModels;

namespace UXAnalytics
{
    public class MainPageViewModel : BaseViewModel
    {
        private ILogger<MainPageViewModel> logger;
        public MainPageViewModel()
        {
            logger = new Logger<MainPageViewModel>();
        }

        public void OnViewAppearing()
        {
            logger.StartLogging();
        }

        public void OnViewDisappearing()
        {
            logger.EndLogging();
        }
    }
}
