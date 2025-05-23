using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace TestConsole
{
	internal class IdentityMessage : IRabbitMQ
	{
		private IConnection connection;
		private IChannel channel;

		private async Task Init() 
		{
			var factory = new ConnectionFactory() { HostName = "localhost" };
			connection = await factory.CreateConnectionAsync();
			channel = await connection.CreateChannelAsync();
			await channel.QueueDeclareAsync(queue: "hello", durable: false, autoDelete: false, exclusive: false, arguments: null);
		}

		public async Task SendMessage(object obj)
		{
			var message = JsonSerializer.Serialize(obj);
			await SendMessage(message);
		}

		public async Task SendMessage(string message)
		{
			if(connection == null || channel == null)
				await Init();

			var body = Encoding.UTF8.GetBytes(message);
			await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "hello", body: body);

		}
	}
}
