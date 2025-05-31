using API.StorefrontService;
using MassTransit;
using StorefrontService.Interfaces;

namespace StorefrontService.Consumers
{
	public class ProductInfoConsumer(IProductItemConfiguratorBuilder builder) : IConsumer<ProductInfoRequest>
	{
		private readonly IProductItemConfiguratorBuilder _builder = builder;
		public async Task Consume(ConsumeContext<ProductInfoRequest> context)
		{
			try
			{
				var reg = context.Message;
				var result = await _builder.BuildAsync(reg.ProductId);
				await context.RespondAsync(result);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error in {typeof(ProductInfoConsumer)}: {ex.Message}");
				if (ex.InnerException != null)
					Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
				throw;
			}
		}
	}
}
