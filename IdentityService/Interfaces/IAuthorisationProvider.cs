using API;
using API.ClientWebAppIdentityService;

namespace IdentityService.Interfaces
{
	internal interface IAuthorisationProvider
	{
		public Task<ServiceResponse<AuthResponce>> AuthorizeAsync(string providedLogin, string providedPassword);
	}
}
