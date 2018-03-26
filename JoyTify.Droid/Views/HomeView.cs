using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace JoyTify.Droid.Views
{
    [Activity(Label = "Home", ScreenOrientation = ScreenOrientation.Portrait)]
    public class HomeView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_HomeView);
        }
    }
}