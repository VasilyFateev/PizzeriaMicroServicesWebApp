using API;
using API.ClientWebAppIdentityService;

namespace IdentityService.Interfaces
{
	internal interface IRegistrationProvider
	{
		public Task<ServiceResponse<AuthResponce>> RegistrateAsync(string providedLogin, string providedPassword);
	}
}
