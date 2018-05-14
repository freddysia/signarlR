using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SignalRDemo1.RabbitMq
{
    public class RabbitMQMiddleware : OwinMiddleware
    {
        private readonly MoveShapeHub hub = new MoveShapeHub();
        public RabbitMQMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            //SubscribeMQ();
            await Next.Invoke(context);

        }

        //public void SubscribeMQ()
        //{
        //    try
        //    {
        //        var factory = new ConnectionFactory();
        //        factory.HostName = "sd-8cb2-0c66.nam.nsroot.net";
        //        factory.UserName = "Administrator";
        //        factory.Password = "Simpliciti2014";

        //        using (var connection = factory.CreateConnection())
        //        using (var channel = connection.CreateModel())
        //        {
        //            channel.QueueDeclare(queue: "suiteQueue",
        //                                 durable: true,
        //                                 exclusive: false,
        //                                 autoDelete: false,
        //                                 arguments: null);

        //            var consumer = new EventingBasicConsumer(channel);
        //            while (true)
        //            {
        //                consumer.Received += (model, ea) =>
        //                {
        //                    var body = ea.Body;
        //                    var message = Encoding.UTF8.GetString(body);
        //                    Console.WriteLine("{0}", message);
        //                    var shapeModel = new ShapeModel()
        //                    {
        //                        Left = 30,
        //                        Top = 40,

        //                    };
        //                    hub.UpdateModel(shapeModel);

        //                };
        //                channel.BasicConsume(queue: "suiteQueue",
        //                                     autoAck: true,
        //                                     consumer: consumer);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}