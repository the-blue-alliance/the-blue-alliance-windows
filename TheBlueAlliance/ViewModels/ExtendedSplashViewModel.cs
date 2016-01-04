namespace TBA.ViewModels
{
    public class ExtendedSplashViewModel : NotificationBase
    {
        public ExtendedSplashViewModel()
        {
            LoadingText = "Loading...";
        }

        private string loadingText;
        public string LoadingText
        {
            get { return loadingText; }
            set
            {
                if (loadingText != value)
                {
                    SetProperty(ref loadingText, value);
                }
            }
        }
    }
}