using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace ViralBlog.Server;

[Authorize]
public class MessagingHub : Hub
{
    public void SendMessage(string message) => Console.WriteLine(message);
}
