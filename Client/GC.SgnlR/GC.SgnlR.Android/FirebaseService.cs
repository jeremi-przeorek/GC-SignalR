using Firebase.Auth;
using GC.SgnlR.Droid.Helpers;
using GC.SignalR;
using System;
using System.Threading.Tasks;

namespace GC.SgnlR.Droid
{
    internal class FirebaseService : IFirebaseService
    {
        public async Task<string> LoginWithCredentials(string idToken, string accessToken)
        {
            var authCredential = GoogleAuthProvider.GetCredential(idToken, accessToken);
            var auth = await FirebaseAuth.Instance.SignInWithCredentialAsync(authCredential);
            var result = await auth.User.GetIdToken(true).ToAwaitableTask();
            return (result as GetTokenResult)?.Token;
        }
    }
}