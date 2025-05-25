using API;
using API.ClientWebAppIdentityService;

namespace IdentityService.Interfaces
{
	public interface IRegistrationProvider
	{
		public Task<ServiceResponse<AuthResponce>> RegistrateAsync(string providedLogin, string providedPassword);
	}
}
