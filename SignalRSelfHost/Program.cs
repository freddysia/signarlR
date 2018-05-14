using System;
using Microsoft.Owin.Hosting;

namespace SignalRSelfHost
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://localhost:8180";
            using (WebApp.Start(url))
            {
                Console.WriteLine("Server running on {0}", url);
                Console.ReadLine();
            }
        }
    }
}
