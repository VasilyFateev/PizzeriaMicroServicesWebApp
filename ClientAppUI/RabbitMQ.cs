using RabbitMQ.Client;
using System.Text.Json;

namespace ClientAppUI
{
	public class RabbitMQService : IRabbitMqService
	{
		public void SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			SendMessage(message);
		}

		public async void SendMessage(string message)
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			using var connection = await factory.CreateConnectionAsync();
			using var channel = await connection.CreateChannelAsync();
		}
	}
}