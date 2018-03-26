using System;
using UIKit;

namespace JoyTify.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            //UIApplication.Main(args, null, "AppDelegate");
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (Exception ex)
            {
                //var asdasd = ClientApp.Singleton;
                //Debug.WriteLine("+++> ! ! ! MAIN EXCEPTION: " + ex);
            }
        }
    }
}