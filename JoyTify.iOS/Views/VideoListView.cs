using Cirrious.FluentLayouts.Touch;
using Foundation;
using UIKit;
using JoyTify.Core.ViewModels;
using MvvmCross.Binding.BindingContext;
using System;
using MvvmCross.Binding.iOS.Views;
using System.Windows.Input;
using MvvmCross.Binding.ExtensionMethods;

namespace JoyTify.iOS.Views
{
    //public class VideoListTableViewSource : MvxStandardTableViewSource
    //{
    //    public VideoListTableViewSource(UITableView tableView) : base(tableView)
    //    {
    //    }
    //}

    //public class VideoListTableViewSource : MvxSimpleTableViewSource
    //{
    //    public ICommand FetchCommand { get; set; }

    //    public VideoListTableViewSource(UITableView tableView) : base(tableView, typeof(VideoListCell))
    //    {
    //        DeselectAutomatically = true;
    //    }

    //    protected override UITableViewCell GetOrCreateCellFor(UITableView tableView, Foundation.NSIndexPath indexPath, object item)
    //    {
    //        var cell = base.GetOrCreateCellFor(tableView, indexPath, item);
    //        if (indexPath.Item == ItemsSource.Count() - 5) FetchCommand?.Execute(null);
    //        return cell;
    //    }
    //}
}
namespace JoyTify.iOS.Views
{
    [Register("VideoListView")]
    //[MvxPanelPresentation(MvxPanelEnum.Center, MvxPanelHintType.ResetRoot, true)]
    public class VideoListView : BaseViewController<VideoListViewModel>, IMvxBindingContextOwner
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var viewModel = this.ViewModel;

            var tab = new UITableView(View.Frame)
            {
                AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            };
            Add(tab);
            View.AddConstraints(
                tab.AtLeftOf(View),
                tab.AtTopOf(View),
                tab.WithSameWidth(View),
                tab.WithSameHeight(View));
            tab.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var source = new MvxStandardTableViewSource(tab, "TitleText Title; SelectedCommand ShowCommand");
            tab.Source = source;
            var set = this.CreateBindingSet<VideoListView, VideoListViewModel>();
            set.Bind(source).To(vm => vm.VideoListItems);
            //set.Bind(source).For(s => s.SelectionChangedCommand).To(vm => vm.GotoTestCommand);
            set.Apply();

            //var scrollView = new UIScrollView(View.Frame)
            //{
            //    ShowsHorizontalScrollIndicator = false,
            //    AutoresizingMask = UIViewAutoresizing.FlexibleHeight
            //};

            //var infoButton = new UIButton();
            //infoButton.SetTitle("Show Info ViewModel", UIControlState.Normal);
            //infoButton.BackgroundColor = UIColor.Blue;
            //scrollView.AddSubviews(infoButton);

            //Add(scrollView);

            //View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            //View.AddConstraints(
            //    scrollView.AtLeftOf(View),
            //    scrollView.AtTopOf(View),
            //    scrollView.WithSameWidth(View),
            //    scrollView.WithSameHeight(View));
            //scrollView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            //var set = this.CreateBindingSet<VideoListView, VideoListViewModel>();
            //set.Bind(infoButton).To("GoToInfoCommand");
            //set.Apply();

            //scrollView.AddConstraints(

            //    infoButton.Below(scrollView).Plus(10),
            //    infoButton.WithSameWidth(scrollView),
            //    infoButton.WithSameLeft(scrollView)
            //    );

        }

        public override void ViewWillAppear(bool animated)
        {
            Title = "VideoList View";
            base.ViewWillAppear(animated);
        }
    }
}
