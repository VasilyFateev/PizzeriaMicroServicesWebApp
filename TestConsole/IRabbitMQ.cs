
namespace TestConsole
{
	internal interface IRabbitMQ
	{
		Task SendMessage(object obj);
		Task SendMessage(string message);
	}
}