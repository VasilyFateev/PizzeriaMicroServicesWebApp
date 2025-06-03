using ClientAppUI.Models.Identity;
using ClientAppUI.RequestSenders.IdentityService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ClientAppUI.Controllers
{
	public class IdentityController(ILogger<IdentityController> logger) : Controller
	{
		private readonly ILogger<IdentityController> _logger = logger;

		[HttpGet]
		public IActionResult Auth()
		{
			return View(new AuthFormModel());
		}
		[HttpGet]
		public IActionResult Reg()
		{
			return View(new RegFormModel());
		}

		[HttpGet]
		public async Task<IActionResult> Ping([FromServices] PingRequestSenderService senderService)
		{
			var responce = await senderService.SendMessageAsync();
			return Ok(responce.StatusCode);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Auth(AuthFormModel model, [FromServices] AuthRequestSenderService senderService)
		{
			if (!ModelState.IsValid)
			{

				ModelState.AddModelError("", "Invalid Model error");
				return View(model);
			}

			try
			{
				var response = await senderService.SendMessageAsync(model.Login, model.Password);
				if (response.StatusCode == StatusCodes.Status200OK)
				{
					if (response.Data == null)
					{
						ModelState.AddModelError("", "Server error");
						return View(model);
					}
					Response.Cookies.Append("jwtToken", response.Data.JwtToken, new CookieOptions
					{
						HttpOnly = true,
						Secure = true,
						SameSite = SameSiteMode.Strict
					});
					Response.Cookies.Append("Name", response.Data.Name, new CookieOptions
					{
						HttpOnly = true,
						Secure = true,
						SameSite = SameSiteMode.Strict
					});
					return RedirectToAction("Index", "Home");
				}
				ModelState.AddModelError("", response.StatusCode switch
				{
					StatusCodes.Status401Unauthorized => "Неверный логин или пароль",
					StatusCodes.Status403Forbidden => "Доступ запрещён",
					StatusCodes.Status404NotFound => "Пользователь не найден",
					_ => "Произошла ошибка при авторизации"
				});
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Ошибка при авторизации");
				ModelState.AddModelError("", "Произошла ошибка при обращении к серверу");
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Reg(RegFormModel model, [FromServices] RegRequestSenderService senderService)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Invalid Model error");
				return View(model);
			}
			var responce = await senderService.SendMessageAsync(model.Name, model.Login, model.Password);
			switch (responce.StatusCode)
			{
				case StatusCodes.Status200OK:
					if (responce.Data == null)
					{
						ModelState.AddModelError("", "Server error");
						return View(model);
					}
					Response.Cookies.Append("jwtToken", responce.Data.JwtToken, new CookieOptions
					{
						HttpOnly = true,
						Secure = true,
						SameSite = SameSiteMode.Strict
					});
					Response.Cookies.Append("Name", responce.Data.Name, new CookieOptions
					{
						HttpOnly = true,
						Secure = true,
						SameSite = SameSiteMode.Strict
					});
					return RedirectToAction("Index", "Home");
				case StatusCodes.Status409Conflict:
					ModelState.AddModelError("", "A user with such an email address or number already exists");
					return View(model);
				default:
					ModelState.AddModelError("", "Invalid login attempt");
					return View(model);
			}
		}
	}
}
