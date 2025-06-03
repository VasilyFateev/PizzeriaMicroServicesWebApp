using API;
using API.StorefrontService;
using API.StorefrontService.Models;

namespace StorefrontService.Interfaces
{
	public interface IAssortmentListBuilder
	{
		public Task<ServiceResponse<AssortmentListResponce>> BuildAsync();
	}
}
