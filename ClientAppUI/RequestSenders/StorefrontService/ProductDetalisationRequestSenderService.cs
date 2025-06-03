using API.StorefrontService;
using MassTransit;
using API;

namespace ClientAppUI.RequestSenders.StorefrontService
{
	public class ProductDetalisationRequestSenderService(IRequestClient<ProductDetalisationRequest> requestClient)
	{
		public async Task<ServiceResponse<ProductDetalisationResponce>> SendMessageAsync(long productId)
		{
			var request = new ProductDetalisationRequest(productId);
			var response = await requestClient.GetResponse<ServiceResponse<ProductDetalisationResponce>>(request);
			return response.Message;
		}
	}
}
