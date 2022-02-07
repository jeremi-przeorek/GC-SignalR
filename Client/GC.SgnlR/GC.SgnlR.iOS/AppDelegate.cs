using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using GC.SignalR;
using UIKit;
using Xamarin.Forms;

namespace GC.SgnlR.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Firebase.Core.App.Configure();
            LoadApplication(new App());

            DependencyService.Register<IGoogleAuth, GoogleAuth>();
            DependencyService.Register<IFirebaseService, FirebaseService>();

            return base.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // Convert iOS NSUrl to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(url.AbsoluteString);

            var googleAuth = DependencyService.Resolve<IGoogleAuth>();

            // load redirect_url Page for parsing
            googleAuth.Auth.OnPageLoading(uri_netfx);

            return true;
        }
    }
}
