using MassTransit;
using API.ClientWebAppIdentityService;
using IdentityService.Interfaces;

namespace IdentityService.Consumers
{
	public class RegConsumer(IRegistrationProvider registrationProvider) : IConsumer<RegRequest>
	{
		private readonly IRegistrationProvider _provider = registrationProvider;

		public async Task Consume(ConsumeContext<RegRequest> context)
		{
			var req = context.Message;
			try
			{
				var result = await _provider.RegistrateAsync(req.Name, req.Login, req.Password);
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
