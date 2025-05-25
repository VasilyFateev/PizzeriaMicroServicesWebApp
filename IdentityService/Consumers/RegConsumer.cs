using MassTransit;
using API.ClientWebAppIdentityService;
using IdentityService.Interfaces;

namespace IdentityService.Consumers
{
	public class RegConsumer : IConsumer<RegRequest>
	{
		private readonly IRegistrationProvider _provider;

		public RegConsumer(IRegistrationProvider registrationProvider)
		{
			_provider = registrationProvider;
		}

		public async Task Consume(ConsumeContext<RegRequest> context)
		{
			var req = context.Message;
			var result = await _provider.RegistrateAsync(req.Login, req.Password);
			await context.RespondAsync(result);
		}
	}
}
