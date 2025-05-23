using IdentityService.Interfaces;
using IdentityService.ServiceComponents;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		[HttpPost]
		public async Task<IResult> RegistrateHandler(AutentificationData data)
		{
			var registerationService = new DefaultRegistrateService(data.ProvidedLogin, data.ProvidedPassword);
			return await registerationService.RegistrateAsync();
		}

		[HttpGet]
		public async Task<IResult> AuthorisationHandler(AutentificationData data)
		{
			var authorisationService = new DefaultAuthorisationService(data.ProvidedLogin, data.ProvidedPassword);
			return await authorisationService.AuthorizeAsync();
		}
	}
}
