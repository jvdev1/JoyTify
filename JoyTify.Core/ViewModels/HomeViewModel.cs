using System.Collections.Generic;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using MvvmCross.Core.Navigation;
using MvvmCross.Platform;
using Google.Apis.YouTube.v3;
using Google.Apis.Services;
using Google.Apis.YouTube.v3.Data;
using System;
using JoyTify.Core.DBModels;
using System.Linq;
using JoyTify.Core.Interfaces;
using JoyTify.Core.Models;
using System.Collections.ObjectModel;

namespace JoyTify.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        public static HomeViewModel tmpSin;

        readonly IMvxNavigationService NavigationService;

        public HomeViewModel()
        {
            tmpSin = this;

            NavigationService = Mvx.Resolve<IMvxNavigationService>();

            HomeItems = new ObservableCollection<LastSearchUIItem>
            {
                new LastSearchUIItem(this, "Seach term 1"),
            };

            HomeItems.Add(new LastSearchUIItem(this, "seach term 2"));

            using (var db = new DatabaseContext(App._DBPath))
            {
                try
                {
                    db.Database.EnsureCreated();

                    List<DBLastSearch> loads = db.ELastSearchs.ToList();
                    for (int i = 0; i < loads.Count; i++)
                    {
                        HomeItems.Add(new LastSearchUIItem(this, loads[i].Term));
                    }
                }
                catch (Exception ex)
                {  }
            }
        }


        private ObservableCollection<LastSearchUIItem> hi;
        public ObservableCollection<LastSearchUIItem> HomeItems
        {
            get
            {
                return hi;
            }
            set
            {
                hi = value;
                RaisePropertyChanged(() => HomeItems);
            }
        }

        public class LastSearchUIItem
        {
            public LastSearchUIItem(HomeViewModel parent, string title)
            {
                Title = title;

                var req = new VideoSearchRequest(title, null);
                ShowCommand = new MvxCommand(() => parent.NavigationService.Navigate<VideoListViewModel, IVideoListingRequest>(req));
            }


            private string _tit; public string Title
            {
                set
                {
                    _tit = value;
                }
                get
                {
                    return _tit;
                }
            }

            public ICommand ShowCommand { get; private set; }

            public override string ToString() { return Title; }
        }
    }
}