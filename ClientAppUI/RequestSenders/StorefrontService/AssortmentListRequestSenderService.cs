using API.StorefrontService;
using MassTransit;
using API;

namespace ClientAppUI.RequestSenders.StorefrontService
{
	public class AssortmentListRequestSenderService(IRequestClient<AssortmentListRequest> requestClient)
	{
		public async Task<ServiceResponse<AssortmentListResponce>> SendMessageAsync()
		{
			var request = new AssortmentListRequest();
			var response = await requestClient.GetResponse<ServiceResponse<AssortmentListResponce>>(request);
			return response.Message;
		}
	}
}
