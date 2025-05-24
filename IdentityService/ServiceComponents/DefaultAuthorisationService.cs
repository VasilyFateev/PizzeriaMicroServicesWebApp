using API;
using API.ClientWebAppIdentityService;
using DatabasesAccess.AccountDb;
using IdentityService.Interfaces;
using IdentityService.Utils;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.ServiceComponents
{
	internal sealed class DefaultAuthorisationService(IJwtTokenGenerator jwtTokenGenerator) : IAuthorisationProvider
	{
		public async Task<ServiceResponse<AuthResponce>> AuthorizeAsync(string providedLogin, string providedPassword)
		{
			if (string.IsNullOrEmpty(providedLogin) || string.IsNullOrEmpty(providedPassword))
				return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status401Unauthorized };

			bool isEmail = RegexCollection.EmailRegex().Match(providedLogin).Success;
			bool isPhone = RegexCollection.PhoneRegex().Match(providedLogin).Success;

			if (!isEmail && !isPhone)
			{
				return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status401Unauthorized };
			}

			using AccountContext context = new();

			var user = await context.Users.FirstOrDefaultAsync(u => (isEmail ? u.Email : u.PhoneNumber) == providedLogin);
			if (user == null)
			{
				return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status401Unauthorized };
			}

			var hashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);
			if (user.HashedPassword == hashedPassword)
			{

				var token = jwtTokenGenerator.GetToken(providedLogin);
				return new ServiceResponse<AuthResponce>()
				{
					StatusCode = StatusCodes.Status200OK,
					Data = new AuthResponce(token)
				};
			}
			else
			{
				return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status401Unauthorized };
			}
		}
	}
}
