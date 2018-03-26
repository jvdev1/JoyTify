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
using System.Collections.ObjectModel;
using JoyTify.Core.Services;
using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions.Enums;
using Plugin.MediaManager.Abstractions.Implementations;
using Plugin.MediaManager.Abstractions;

namespace JoyTify.Core.ViewModels
{
    public class VideoListViewModel : MvxViewModel<IVideoListingRequest>
    {
        readonly IMvxNavigationService NavigationService;
        public VideoListViewModel()
        {
            NavigationService = Mvx.Resolve<IMvxNavigationService>();
        }

        public override void Prepare(IVideoListingRequest parameter)
        {
            VideoListItems = new ObservableCollection<VideoUIItem>
            {
                new VideoUIItem(this, new DBVideo{ Title = "loading..." }),
            };
            Search(parameter.ByVideoId, parameter.ByQuery);
        }

        public async void Search(string basedOnVideoId, string query = null)
        {
            YouTubeService service;
            service = new YouTubeService(new BaseClientService.Initializer()
            { HttpClientInitializer = App._CRED, ApplicationName = "EwTewb" });


            var pls = service.Search.List("snippet");
            pls.Type = "video"; pls.MaxResults = 10;
            if (basedOnVideoId != null) pls.RelatedToVideoId = basedOnVideoId;
            if (query != null) pls.Q = query;

            SearchListResponse plRes = null;

            try { plRes = await pls.ExecuteAsync(); }
            catch (Exception ex)
            { }


            VideoListItems = new ObservableCollection<VideoUIItem>();

            foreach (var res in plRes.Items)
            {
                if (res.Id.Kind != "youtube#video") continue;

                var evid = new DBVideo
                {
                    VideoId = res.Id.VideoId,
                    ChannelId = res.Snippet.ChannelId,
                    ChannelTitle = res.Snippet.ChannelTitle,
                    Description = res.Snippet.Description,
                    Date = res.Snippet.PublishedAt ?? default(DateTime),
                    Thumb = res.Snippet.Thumbnails == null ? "" : (res.Snippet.Thumbnails.Medium?.Url ?? res.Snippet.Thumbnails.Default__?.Url ?? res.Snippet.Thumbnails.Standard?.Url),
                    Title = res.Snippet.Title,
                };

                var uivid = new VideoUIItem(this, evid)
                {
                };

                VideoListItems.Add(uivid);
            }
        }



        private ObservableCollection<VideoUIItem> ml;
        public ObservableCollection<VideoUIItem> VideoListItems
        {
            get { return ml; }
            set
            {
                ml = value;
                RaisePropertyChanged(() => VideoListItems);
            }
        }

        public class VideoUIItem
        {
            public string Title { get; private set; }

            public VideoUIItem(VideoListViewModel parent, DBVideo dbVid)
            {
                DBVid = dbVid;
                Title = DBVid?.Title ?? "?";

                //var req = new VideoSearchRequest
                //{
                //};
                ShowCommand = new MvxCommand(() => 
                {
                    Pull();
                });
            }

            async void Pull()
            {
                string link = await Puller.New_Pull(DBVid?.VideoId ?? "");
                if (!string.IsNullOrEmpty(link))
                {
                    IMediaFile mediaFile = new MediaFile(link, MediaFileType.Audio, ResourceAvailability.Remote)
                    {
                        Metadata = new MediaFileMetadata
                        {
                            Title = "ecks dee",
                            Artist = "Volunteer Park Trust",
                            Album = "Walking Tour"
                        }
                    };
                    //mediaFile = await CrossMediaManager.Current.MediaExtractor.ExtractMediaInfo(mediaFile);
                    await CrossMediaManager.Current.Play(mediaFile);
                    CrossMediaManager.Current.MediaNotificationManager.StartNotification(mediaFile);
                    CrossMediaManager.Current.MediaNotificationManager.UpdateNotifications(mediaFile, MediaPlayerStatus.Playing);
                }

            }

            public DBVideo DBVid { get; private set; }
            public ICommand ShowCommand { get; private set; }
        }
    }
}
