//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Support.V7.Widget;
//using Android.Views;
//using Android.Widget;
//using JoyTify.Core.ViewModels;
//using MvvmCross.Binding.Droid.BindingContext;
//using MvvmCross.Droid.Support.V4;
//using MvvmCross.Droid.Support.V7.RecyclerView;
//using MvvmCross.Droid.Views.Attributes;
//using MvvmCross.Platform.Droid.WeakSubscription;

//namespace JoyTify.Droid.Views.SubViews
//{
//    [MvxFragmentPresentation(typeof(VideoListViewModel), Resource.Id.content_frame, true)]
//    [Register("ewtunes.droid.views.subviews.videolistfragment")]
//    public class VideoListFragment : MvxFragment
//    {
//        protected VideoListFragment()
//        {
//            RetainInstance = true;
//        }
        

//        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
//        {
//            var view = base.OnCreateView(inflater, container, savedInstanceState);

//            var recyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.my_recycler_view);
//            if (recyclerView != null)
//            {
//                recyclerView.HasFixedSize = true;
//                var layoutManager = new LinearLayoutManager(Activity);
//                recyclerView.SetLayoutManager(layoutManager);
//            }

//            //_itemSelectedToken = ViewModel.WeakSubscribe(() => ViewModel.SelectedItem,
//            //    (sender, args) => {
//            //        if (ViewModel.SelectedItem != null)
//            //            Toast.MakeText(Activity,
//            //                $"Selected: {ViewModel.SelectedItem.Title}",
//            //                ToastLength.Short).Show();
//            //    });

//            return view;
//        }
//    }
//}