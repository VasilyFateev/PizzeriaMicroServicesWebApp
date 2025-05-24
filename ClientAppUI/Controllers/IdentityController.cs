using ClientAppUI.RequestSenders;
using Microsoft.AspNetCore.Mvc;

namespace ClientAppUI.Controllers
{
	public class IdentityController(AuthRequestSenderService authRequestSender, RegRequestSenderService regRequestSender) : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IResult> AuthAsync(HttpContext context, string providedLogin, string providedPassword)
		{
			var responce = await authRequestSender.SendMessageAsync(providedLogin, providedPassword);
			IResult result;	
			switch (responce.StatusCode)
			{
				case 200:
					result = Results.Ok();
					await AddJwtTokenInCookiesAsync(context, responce.Data.JwtToken);
					break;
				default:
					result = Results.Unauthorized();
					break;
			}
			return result;
		}

		[HttpPut]
		public async Task<IResult> RegAsync(HttpContext context, string providedLogin, string providedPassword)
		{
			var responce = await regRequestSender.SendMessageAsync(providedLogin, providedPassword);
			IResult result;
			switch (responce.StatusCode)
			{
				case 200:
					result = Results.Ok();
					await AddJwtTokenInCookiesAsync(context, responce.Data.JwtToken);
					break;
				default:
					result = Results.Unauthorized();
					break;
			}
			return result;
		}

		private static async Task AddJwtTokenInCookiesAsync(HttpContext context, string token)
		{
			context.Response.Cookies.Append("access_token", token, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.Strict,
				Expires = DateTime.UtcNow.AddHours(1)
			});

			await context.Response.WriteAsJsonAsync(new { token });
		}
	}
}
