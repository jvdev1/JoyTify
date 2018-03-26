using Android.App;
using Android.Content.PM;
using MvvmCross.Droid.Views;

namespace JoyTify.Droid.Views
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
    public class VideoListView : MvxActivity
    {
        protected override void OnViewModelSet()
        {
            SetContentView(Resource.Layout.Page_VideoListView);
        }


    }
}