using MassTransit;
using API.ClientWebAppIdentityService;
using IdentityService.Interfaces;

namespace IdentityService.Consumers
{
	internal class AuthConsumer : IConsumer<AuthRequest>
	{
		private readonly IAuthorisationProvider _provider;

		internal AuthConsumer(IAuthorisationProvider authorisationProvider)
		{
			_provider = authorisationProvider;
		}

		public async Task Consume(ConsumeContext<AuthRequest> context)
		{
			var req = context.Message;
			var result = await _provider.AuthorizeAsync(req.Login, req.Password);
			await context.RespondAsync(result);
		}
	}
}
