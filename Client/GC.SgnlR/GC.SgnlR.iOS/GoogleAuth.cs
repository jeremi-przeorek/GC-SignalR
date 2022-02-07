using System;
using UIKit;
using Xamarin.Auth;

namespace GC.SgnlR.iOS
{
    public class GoogleAuth : IGoogleAuth
    {
        public event EventHandler<AuthenticatorCompletedEventArgs> AuthSuccess;
        public event EventHandler<AuthenticatorCompletedEventArgs> AuthFailure;

        public OAuth2Authenticator Auth { get; private set; }

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

            var vc = UIApplication.SharedApplication.KeyWindow.RootViewController;
            vc.PresentViewController(Auth.GetUI(), true, null);
        }

        private void Auth_Complted(object sender, AuthenticatorCompletedEventArgs e)
        {
            UIApplication.SharedApplication.KeyWindow.RootViewController.DismissViewController(true, null);

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