using System;
using System.Collections.Generic;
using System.Linq;
using Cirrious.FluentLayouts.Touch;
using Foundation;
using MvvmCross.Binding.BindingContext;
using UIKit;
using static JoyTify.Core.ViewModels.HomeViewModel;

namespace JoyTify.iOS.Views
{
    public class HomeCell : BaseTableViewCell
    {
        private const float PADDING = 12f;

        private UILabel _lblName;

        public HomeCell(IntPtr handle) : base(handle)
        {
        }

        protected override void CreateView()
        {
            base.CreateView();

            SelectionStyle = UITableViewCellSelectionStyle.None;

            _lblName = new UILabel
            {
                TextColor = UIColor.Red,
                Font = UIFont.SystemFontOfSize(15f, UIFontWeight.Bold)
            };

            BackgroundColor = UIColor.Clear;
            ContentView.AddSubview(_lblName);
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            this.DelayBind(
                () =>
                {
                    this.AddBindings(_lblName, nameof(LastSearchUIItem.Title));
                });
        }

        protected override void CreateConstraints()
        {
            base.CreateConstraints();

            ContentView.AddConstraints(
                _lblName.AtLeftOf(ContentView, PADDING),
                _lblName.AtTopOf(ContentView, PADDING),
                _lblName.AtBottomOf(ContentView, PADDING),
                _lblName.AtRightOf(ContentView, PADDING)
            );
        }
    }
}