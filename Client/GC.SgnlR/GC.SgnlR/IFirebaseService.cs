using System.Threading.Tasks;

namespace GC.SignalR
{
    public interface IFirebaseService
    {
        Task<string> LoginWithCredentials(string idToken, string accessToken);
    }
}