using API.ClientWebAppIdentityService;
using MassTransit;
using API;

namespace ClientAppUI.RequestSenders
{
	public class AuthRequestSenderService(IRequestClient<AuthRequest> requestClient)
	{
		public async Task<ServiceResponse<AuthResponce>> SendMessageAsync(string providedLogin, string providedPassword)
		{
			var request = new AuthRequest(providedLogin, providedPassword);
			var response = await requestClient.GetResponse<ServiceResponse<AuthResponce>>(request);
			return response.Message;
		}
	}
}
