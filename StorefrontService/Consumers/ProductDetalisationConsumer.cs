using API.StorefrontService;
using MassTransit;
using StorefrontService.Interfaces;

namespace StorefrontService.Consumers
{
	public class ProductDetalisationConsumer(IProductDetalisationBuilder builder) : IConsumer<ProductDetalisationRequest>
	{
		private readonly IProductDetalisationBuilder _builder = builder;
		public async Task Consume(ConsumeContext<ProductDetalisationRequest> context)
		{
			try
			{
				var reg = context.Message;
				var result = await _builder.BuildAsync(reg.ProductId);
				await context.RespondAsync(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in {typeof(ProductDetalisationConsumer)}: {ex.Message}");
				if (ex.InnerException != null)
					Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
				throw;
			}
		}
	}
}
