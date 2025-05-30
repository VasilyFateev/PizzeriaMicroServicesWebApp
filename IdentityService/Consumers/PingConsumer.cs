using API;
using API.ClientWebAppIdentityService;
using MassTransit;

namespace IdentityService.Consumers
{
	public class PingConsumer : IConsumer<PingRequest>
	{
		public async Task Consume(ConsumeContext<PingRequest> context)
		{
			await context.RespondAsync(new ServiceResponse<PingResponce>(StatusCodes.Status200OK, string.Empty, new PingResponce()));
		}
	}
}
