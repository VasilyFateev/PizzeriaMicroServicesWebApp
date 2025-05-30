using MassTransit;
using API.ClientWebAppIdentityService;
using IdentityService.Interfaces;

namespace IdentityService.Consumers
{
	public class AuthConsumer(IAuthorisationProvider authorisationProvider) : IConsumer<AuthRequest>
	{
		private readonly IAuthorisationProvider _provider = authorisationProvider;

		public async Task Consume(ConsumeContext<AuthRequest> context)
		{
			try
			{
				var req = context.Message;
				var result = await _provider.AuthorizeAsync(req.Login, req.Password);
				await context.RespondAsync(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in RegConsumer: {ex.Message}");
				if (ex.InnerException != null)
					Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
				throw;
			}
		}
	}
}
