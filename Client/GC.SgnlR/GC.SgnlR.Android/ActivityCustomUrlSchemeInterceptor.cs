using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using System;
using Xamarin.Forms;

namespace GC.SgnlR.Droid
{
    [Activity(Label = "ActivityCustomUrlSchemeInterceptor", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [
        IntentFilter
        (
            actions: new[] { Intent.ActionView },
            Categories = new[]
                    {
                    Intent.CategoryDefault,
                    Intent.CategoryBrowsable
                    },
            DataSchemes = new[]
                    {
                        "com.googleusercontent.apps.1085728934350-c0u1sn9f57ett419lravmgbvvj8u3cdb",
                    },

            DataPath = "/oauth2redirect"
        )
    ]
    public class ActivityCustomUrlSchemeInterceptor : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            global::Android.Net.Uri uri_android = Intent.Data;

            var googleAuth = DependencyService.Resolve<IGoogleAuth>();
            // Convert Android.Net.Url to C#/netxf/BCL System.Uri - common API
            Uri uri_netfx = new Uri(uri_android.ToString());
            // load redirect_url Page for parsing
            googleAuth.Auth.OnPageLoading(uri_netfx);

            this.Finish();
        }
    }
}