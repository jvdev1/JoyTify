using Android.App;
using Android.Widget;
using Android.OS;
using MvvmCross.Droid.Views;

namespace JoyTify.Droid
{
    [Activity(Label = "JoyTify", MainLauncher = true, NoHistory = true /*, Icon = "@drawable/icon"*/)]
    public class SplashScreenActivity : MvxSplashScreenActivity
    {
        public SplashScreenActivity() : base(Resource.Layout.SplashScreen)
        {

        }
    }
}

