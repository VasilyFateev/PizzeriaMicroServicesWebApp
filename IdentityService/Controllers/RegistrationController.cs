using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController : ControllerBase
	{
		[HttpGet]
		public async Task<IResult> RegistrateHandler(RegistrationData data)
		{
			var login = data.ProvidedLogin;
			var password = data.ProvidedPassword;
			return Results.Ok();
		}
	}
	public record RegistrationData(string ProvidedLogin, string ProvidedPassword);
}
