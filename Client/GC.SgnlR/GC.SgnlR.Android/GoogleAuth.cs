using Android.Content;
using System;
using Xamarin.Auth;

namespace GC.SgnlR.Droid
{
    internal class GoogleAuth : IGoogleAuth
    {
        public OAuth2Authenticator Auth { get; private set; }

        public event EventHandler<AuthenticatorCompletedEventArgs> AuthSuccess;
        public event EventHandler<AuthenticatorCompletedEventArgs> AuthFailure;

        public void LoginAsync()
        {
            Auth = new OAuth2Authenticator(
                clientId: "1085728934350-c0u1sn9f57ett419lravmgbvvj8u3cdb.apps.googleusercontent.com",
                clientSecret: "",
                scope: "profile",
                authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/v2/auth"),
                redirectUrl: new Uri("com.googleusercontent.apps.1085728934350-c0u1sn9f57ett419lravmgbvvj8u3cdb:/oauth2redirect"),
                accessTokenUrl: new Uri("https://www.googleapis.com/oauth2/v4/token"),
                isUsingNativeUI: true);

            Auth.Completed += Auth_Complted;

            var activity = Xamarin.Essentials.Platform.CurrentActivity;

            Intent ui_object = Auth.GetUI(activity);
            activity.StartActivity(ui_object);
        }

        private void Auth_Complted(object sender, AuthenticatorCompletedEventArgs e)
        {
            // Close browser 
            var activity = Xamarin.Essentials.Platform.CurrentActivity;
            var intent = new Intent(activity, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
            activity.StartActivity(intent);

            if (e.IsAuthenticated)
            {
                AuthSuccess?.Invoke(this, e);
            }
            else
            {
                AuthFailure?.Invoke(this, e);
            }
        }
    }
}