using Microservice.Dtos;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microservice.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _configuration;
        private readonly IConnection _connection;
        private IModel _channel;

        public MessageBusClient(IConfiguration configuration)
        {
                _configuration=configuration;
            var factory = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], 
                Port =int.Parse( _configuration["RabbitMQPort"]) };
            try 
            {
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "triger", type: ExchangeType.Fanout);
                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine("-->Connected to MessageBox");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--->Could Not Connect to the Message Box{ex.Message}");
            }

        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--RabbitMq Connection ShutDOwn");
        }

        public void publishnewPlatform(PlatformPublishedDto platforPublishedDto)
        {
            var message = JsonSerializer.Serialize(platforPublishedDto);
            if (_connection.IsOpen)
            {
                Console.Write("---->RabbitMQ COnnection Is Open,Sending Message");

            }
            else
            {
                Console.WriteLine("RabbitMQ is Closed,  not sending ");
            }
        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "triger",
                routingKey: "",
                basicProperties: null,
                body: body);
            Console.WriteLine($"---> we have sent message}");

        }
        public void Dispose()
        {
            Console.WriteLine("Messagebus Dispose");
            if (_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}
