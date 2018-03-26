using JoyTify.Core.ViewModels;
using MvvmCross.Core.ViewModels;

namespace JoyTify.Core
{
    public class App : MvxApplication
    {
        public static Google.Apis.Auth.OAuth2.UserCredential _CRED;
        public static string _DBPath;

        public override void Initialize()
        {
            RegisterNavigationServiceAppStart<HomeViewModel>();
        }
    }
}
