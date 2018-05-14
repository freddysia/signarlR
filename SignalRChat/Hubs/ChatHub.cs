using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRChat.Hubs
{
    [HubName("chatHub")]
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            //Clients.All.addNewMessageToPage(name, message);
            Clients.Client(Context.ConnectionId).addNewMessageToPage(name, message);
            //Clients.Others.addNewMessageToPage(name, message);
        }
    }

    
}