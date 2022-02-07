using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GC.SgnlR
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagingPage : ContentPage
    {
        private string _token;
        private HubConnection _connection;

        public MessagingPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await _connection.InvokeCoreAsync("SendMessage", args: new[] { entry.Text });
        }

        protected async override void OnAppearing()
        {
            _token = BindingContext as string;
            _connection = new HubConnectionBuilder()
                .WithUrl("https://192.168.0.136:45455/messaging", options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_token);
                    options.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            // bypass SSL certificate
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                                    (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                })
                .Build();

            await _connection.StartAsync();

            base.OnAppearing();
        }
    }
}