using API.ClientWebAppIdentityService;
using MassTransit;
using API;

namespace ClientAppUI.RequestSenders.IdentityService
{
	public class PingRequestSenderService(IRequestClient<PingRequest> requestClient)
	{
		public async Task<ServiceResponse<PingResponce>> SendMessageAsync()
		{
			var request = new PingResponce();
			var response = await requestClient.GetResponse<ServiceResponse<PingResponce>>(request);
			return response.Message;
		}
	}
}
