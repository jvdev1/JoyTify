using UIKit;
using MvvmCross.iOS.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Platform;
using MvvmCross.iOS.Views.Presenters;
using JoyTify.Core.Interfaces;
using JoyTify.iOS.Services;
using System;
using System.IO;
using JoyTify.Core;
using Foundation;
using Google.Apis.YouTube.v3;
using System.Threading;
using Google.Apis.Util.Store;

namespace JoyTify.iOS
{
    public class Setup : MvxIosSetup
    {
        public string WorkPath;

        private MvxApplicationDelegate _applicationDelegate;
        private UIWindow _window;
        
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window) : base(applicationDelegate, window)
        {
            WorkPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (!Directory.Exists(WorkPath)) Directory.CreateDirectory(WorkPath);
            App._DBPath = Path.Combine(WorkPath, "JoyTify.db");




            /// secret 
            string fileName = "client_secrets.json";
            var internalPath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            string client_secretsPath = Path.Combine(WorkPath, fileName);
            if (!File.Exists(client_secretsPath)) File.Copy(internalPath, client_secretsPath);


            /// Token 
            fileName = "Google.Apis.Auth.OAuth2.Responses.TokenResponse-user";
            internalPath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
            string tokenPath = Path.Combine(WorkPath, fileName);
            if (!File.Exists(tokenPath)) File.Copy(internalPath, tokenPath);


            /// Cred 
            using (var stream = new FileStream(client_secretsPath, FileMode.Open, FileAccess.Read))
            {
                App._CRED = Google.Apis.Auth.OAuth2.GoogleWebAuthorizationBroker.AuthorizeAsync(
                    Google.Apis.Auth.OAuth2.GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows for read-only access to the authenticated 
                    // user's account, but not other types of account access.
                    new[] { YouTubeService.Scope.Youtube, },
                    "user",
                    CancellationToken.None,
                    new FileDataStore(WorkPath, true)
                ).Result;
            }



            _applicationDelegate = applicationDelegate;
            _window = window;

            SQLitePCL.Batteries_V2.Init();
        }


        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
        
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }

        protected override IMvxIosViewPresenter CreatePresenter()
        {
            return new MvxIosViewPresenter((MvxApplicationDelegate)ApplicationDelegate, Window);
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IDialogService>(() => new TouchDialogService());
            //register the presentation hint to pop to root
            //picked up in the third view model
            //Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint(MvxPanelEnum.Center));
        }
    }



    
}
