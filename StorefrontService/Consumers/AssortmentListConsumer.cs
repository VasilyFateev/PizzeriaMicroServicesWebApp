using StorefrontService.Interfaces;
using API.StorefrontService;
using MassTransit;

namespace StorefrontService.Consumers
{
	public class AssortmentListConsumer(IAssortmentListBuilder builder) : IConsumer<AssortmentListRequest>
	{
		private readonly IAssortmentListBuilder _builder = builder;
		public async Task Consume(ConsumeContext<AssortmentListRequest> context)
		{
			try
			{
				var result = await _builder.BuildAsync();
				await context.RespondAsync(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in {typeof(AssortmentListConsumer)}: {ex.Message}");
				if (ex.InnerException != null)
					Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
				throw;
			}
		}
	}
}
