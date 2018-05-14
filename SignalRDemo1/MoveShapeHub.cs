using System;
using System.Text;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace SignalRDemo1
{
    [HubName("moveShapeHub")]
    public class MoveShapeHub : Hub
    {
        //public void UpdateModel(ShapeModel clientModel)
        //{
        //    //clientModel.LastUpdatedBy = Context.ConnectionId;
        //    //更新位置  
        //    Clients.All.updateShape(clientModel);
        //}
        public void sendMqMessage(string str)
        {
            var factory = new ConnectionFactory();
            factory.HostName = "sd-8cb2-0c66.nam.nsroot.net";
            factory.UserName = "Administrator";
            factory.Password = "Simpliciti2014";

            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "Updates.Trades",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Clients.All.sendMessage(message);
                    };

                    while (true)
                    {

                        channel.BasicConsume(queue: "Updates.Trades",
                                             autoAck: true,
                                             consumer: consumer);

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} exception caught.", e);
            }
        }
    }
    /// <summary>  
    /// 同步客户端和服务器模型  
    /// </summary>  
    //public class ShapeModel
    //{
    //    //给客户端返回left属性  
    //    [JsonProperty("left")]
    //    public double Left { get; set; }

    //    //给客户端返回top属性  
    //    [JsonProperty("top")]
    //    public double Top { get; set; }

    //    //不给客户端返回LastUpdatedBy属性  
    //    [JsonIgnore]
    //    public string LastUpdatedBy { get; set; }
    //}
    public class TradeModel
    {
        public virtual string XRef { get; set; }
        public string XRefType { get; set; }
        public string Identifier { get; set; }
        public string ProductType { get; set; }
        public string TradeID { get; set; }
    }

    public class Circle : TradeModel
    {
        public override string XRef { get; set; }
    }
}