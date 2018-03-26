using Android.Content;
using JoyTify.Core;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.RecyclerView;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;

namespace JoyTify.Droid
{
    public class Setup : MvxAndroidSetup
    {
        public string WorkPath;

        public Setup(Context applicationContext) : base(applicationContext)
        {
            WorkPath = applicationContext.ApplicationInfo.DataDir;
            if (!Directory.Exists(WorkPath)) Directory.CreateDirectory(WorkPath);
            App._DBPath = Path.Combine(WorkPath, "JoyTify.db");




            /// secret 
            Android.Content.Res.AssetManager assets = applicationContext.Assets; string fileName = "client_secrets.json";
            string client_secretsPath = Path.Combine(WorkPath, fileName);
            if (!File.Exists(client_secretsPath)) using (StreamReader sr = new StreamReader(assets.Open(fileName)))
                {
                    string content = sr.ReadToEnd();
                    File.WriteAllText(client_secretsPath, content);
                }

            /// Token 
            fileName = "Google.Apis.Auth.OAuth2.Responses.TokenResponse-user";
            string tokenPath = Path.Combine(WorkPath, fileName);
            if (!File.Exists(tokenPath)) using (StreamReader sr = new StreamReader(assets.Open(fileName)))
                {
                    string content = sr.ReadToEnd();
                    File.WriteAllText(tokenPath, content);
                }

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
        }

        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        protected override void InitializeLastChance()
        {
            base.InitializeLastChance();
        }

        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
            {
                typeof(MvxRecyclerView).Assembly
            };
    }
}