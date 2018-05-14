using Microsoft.AspNet.SignalR;
using SignalRChat.Hubs;

namespace SignalRChat.SignalRServer
{
    public static class SignalrServerToClient
    {
        static readonly IHubContext _myHubContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        public static void BroadcastMessage(string message)
        {
            _myHubContext.Clients.All.broadcastMessage(message);
        }
    }
}