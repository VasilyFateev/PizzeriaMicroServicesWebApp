using API.ClientWebAppIdentityService;
using MassTransit;
using API;

namespace ClientAppUI.RequestSenders.IdentityService
{
	public class RegRequestSenderService(IRequestClient<RegRequest> requestClient)
	{
		public async Task<ServiceResponse<RegResponce>> SendMessageAsync(string providedName, string providedLogin, string providedPassword)
		{
			var request = new RegRequest(providedName, providedLogin, providedPassword);
			var response = await requestClient.GetResponse<ServiceResponse<RegResponce>>(request);
			return response.Message;
		}
	}
}
