using GC.SignalR;
using System;
using Xamarin.Auth;
using Xamarin.Forms;

namespace GC.SgnlR
{
    public partial class MainPage : ContentPage
    {
        private IGoogleAuth _googleAuth;

        public MainPage()
        {
            InitializeComponent();

            _googleAuth = DependencyService.Resolve<IGoogleAuth>();
            _googleAuth.AuthSuccess += GoogleAuth_AuthSuccess;
            _googleAuth.AuthFailure += GoogleAuth_AuthFailure;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            _googleAuth.LoginAsync();
        }

        private void GoogleAuth_AuthFailure(object sender, AuthenticatorCompletedEventArgs e)
        {
            DisplayAlert("Login Failure!", null, "Ok");

        }

        private async void GoogleAuth_AuthSuccess(object sender, AuthenticatorCompletedEventArgs e)
        {
            var firebaseService = DependencyService.Resolve<IFirebaseService>();
            var token = await firebaseService.LoginWithCredentials(e.Account.Properties["id_token"], e.Account.Properties["access_token"]);

            await Navigation.PushAsync(new MessagingPage { BindingContext = token });
        }
    }
}
