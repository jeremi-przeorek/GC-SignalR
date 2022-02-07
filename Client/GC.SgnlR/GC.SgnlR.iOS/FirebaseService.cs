using Firebase.Auth;
using GC.SignalR;
using System.Threading.Tasks;

namespace GC.SgnlR.iOS
{
    public class FirebaseService : IFirebaseService
    {
        public async Task<string> LoginWithCredentials(string idToken, string accessToken)
        {
            var authCredential = GoogleAuthProvider.GetCredential(idToken, accessToken);
            var auth = await Auth.DefaultInstance.SignInWithCredentialAsync(authCredential);
            return await auth.User.GetIdTokenAsync();
        }
    }
}