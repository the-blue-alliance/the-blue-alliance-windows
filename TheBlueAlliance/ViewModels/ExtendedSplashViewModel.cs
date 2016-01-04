using TBA.Common;

namespace TBA.Views
{
    public class ExtendedSplashViewModel : BindableBase
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