using API;
using API.StorefrontService.Models;

namespace StorefrontService.Interfaces
{
	public interface IProductItemConfiguratorBuilder
	{
		public Task<ServiceResponse<ProductItemConfigurator>> BuildAsync(long productId);
	}
}
