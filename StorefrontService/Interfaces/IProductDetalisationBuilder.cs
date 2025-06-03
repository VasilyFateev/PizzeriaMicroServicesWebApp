using API;
using API.StorefrontService;
using API.StorefrontService.Models;

namespace StorefrontService.Interfaces
{
	public interface IProductDetalisationBuilder
	{
		public Task<ServiceResponse<ProductDetalisationResponce>> BuildAsync(long productId);
	}
}
