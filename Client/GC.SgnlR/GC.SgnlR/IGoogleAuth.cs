using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace GC.SgnlR
{
    public interface IGoogleAuth
    {
        OAuth2Authenticator Auth { get; }
        event EventHandler<AuthenticatorCompletedEventArgs> AuthSuccess;
        event EventHandler<AuthenticatorCompletedEventArgs> AuthFailure;
        void LoginAsync();
    }
}
