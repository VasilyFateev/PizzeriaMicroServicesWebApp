using API;
using API.StorefrontService.Models;

namespace StorefrontService.Interfaces
{
	public interface IAssortmentListBuilder
	{
		public Task<ServiceResponse<AssortmentList>> BuildAsync();
	}
}
