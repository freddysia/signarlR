using System.Web.Mvc;
using SignalRChat.SignalRServer;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? id = 5)
        {
            SignalrServerToClient.BroadcastMessage("System message: " + id.ToString());
            return View();
        }
        
        public ActionResult Chat()
        {
            return View();
        }
    }
}