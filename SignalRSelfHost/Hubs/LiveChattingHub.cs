using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace SignalRSelfHost
{
    [HubName("liveChattingHub")]
    public class LiveChattingHub : Hub
    {
        public void send(string name, string message, string timeStamp)
        {
            Clients.All.addMessage(name, message, timeStamp);
        }
    }
}
