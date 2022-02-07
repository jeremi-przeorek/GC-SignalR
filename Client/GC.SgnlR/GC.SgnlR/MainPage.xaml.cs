using GC.SignalR;
using System;
using Xamarin.Auth;
using Xamarin.Forms;

namespace GC.SgnlR
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var googleAuth = DependencyService.Resolve<IGoogleAuth>();

            googleAuth.AuthSuccess += GoogleAuth_AuthSuccess;
            googleAuth.AuthFailure += GoogleAuth_AuthFailure;

            googleAuth.LoginAsync();
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
