using API;
using API.ClientWebAppIdentityService;

namespace IdentityService.Interfaces
{
	public interface IRegistrationProvider
	{
		public Task<ServiceResponse<RegResponce>> RegistrateAsync(string providedName, string providedLogin, string providedPassword);
	}
}
