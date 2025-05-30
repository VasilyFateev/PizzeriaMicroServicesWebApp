using API;
using API.ClientWebAppIdentityService;
using DatabasesAccess.AccountDb;
using IdentityService.Interfaces;
using IdentityService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.ServiceComponents
{
	internal sealed class DefaultAuthorisationService(IJwtTokenGenerator jwtTokenGenerator, [FromServices] AccountContext accountContext) : IAuthorisationProvider
	{
		public async Task<ServiceResponse<AuthResponce>> AuthorizeAsync(string providedLogin, string providedPassword)
		{
			if (string.IsNullOrEmpty(providedLogin) || string.IsNullOrEmpty(providedPassword))
				return new ServiceResponse<AuthResponce>(StatusCodes.Status401Unauthorized, string.Empty, null);

			bool isEmail = RegexCollection.EmailRegex().Match(providedLogin).Success;
			bool isPhone = RegexCollection.PhoneRegex().Match(providedLogin).Success;

			if (!isEmail && !isPhone)
			{
				return new ServiceResponse<AuthResponce>(StatusCodes.Status401Unauthorized, string.Empty, null);
			}

			var user = await accountContext.Users.FirstOrDefaultAsync(u => (isEmail ? u.Email : u.PhoneNumber) == providedLogin);
			if (user == null)
			{
				return new ServiceResponse<AuthResponce>(StatusCodes.Status401Unauthorized, string.Empty, null);
			}

			if (PasswordHasher.VerifyHashedPassword(user, providedPassword))
			{
				var token = jwtTokenGenerator.GetToken(providedLogin);
				return new ServiceResponse<AuthResponce>(StatusCodes.Status200OK, string.Empty, new AuthResponce(user.Name, token));
			}
			else
			{
				return new ServiceResponse<AuthResponce>(StatusCodes.Status401Unauthorized, string.Empty, null);
			}
		}
	}
}
