using API.ClientWebAppIdentityService;
using MassTransit;
using API;

namespace ClientAppUI.RequestSenders
{
	public class AuthRequestSenderService(IRequestClient<AuthRequest> requestClient)
	{
		public async Task<ServiceResponse<AuthResponce>> SendMessageAsync(string porvidedLogin, string providedPassword)
		{
			var request = new AuthRequest(porvidedLogin, providedPassword);
			var response = await requestClient.GetResponse<ServiceResponse<AuthResponce>>(request);
			return response.Message;
		}
	}

	public class RegRequestSenderService(IRequestClient<RegRequest> requestClient)
	{
		public async Task<ServiceResponse<AuthResponce>> SendMessageAsync(string porvidedLogin, string providedPassword)
		{
			var request = new RegRequest(porvidedLogin, providedPassword);
			var response = await requestClient.GetResponse<ServiceResponse<AuthResponce>>(request);
			return response.Message;
		}
	}
}
