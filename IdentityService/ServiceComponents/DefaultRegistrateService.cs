using API;
using API.ClientWebAppIdentityService;
using DatabaseModels.AccountDatabaseModels;
using DatabasesAccess.AccountDb;
using IdentityService.Interfaces;
using IdentityService.Utils;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.ServiceComponents
{
	internal sealed class DefaultRegistrateService(IJwtTokenGenerator jwtTokenGenerator) : IRegistrationProvider
	{
		public async Task<ServiceResponse<AuthResponce>> RegistrateAsync(string providedLogin, string providedPassword)
		{
			if (string.IsNullOrEmpty(providedLogin))
				return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status400BadRequest };

			if (string.IsNullOrEmpty(providedPassword))
				return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status400BadRequest };

			return providedLogin switch
			{
				string when RegexCollection.EmailRegex().IsMatch(providedLogin) => await RegistrateWithEmailAsync(providedLogin, providedPassword),
				string when RegexCollection.PhoneRegex().IsMatch(providedLogin) => await RegistrateWithPhoneAsync(providedLogin, providedPassword),
				_ => new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status400BadRequest }
			};

			async Task<ServiceResponse<AuthResponce>> RegistrateWithEmailAsync(string providedEmail, string providedPassword)
			{
				using AccountContext context = new();
				var sameUser = await context.Users.FirstOrDefaultAsync(u => u.Email == providedEmail);
				if (sameUser != null)
				{
					return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status409Conflict };
				}
				var user = new User
				{
					Email = providedEmail
				};
				user.HashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var token = jwtTokenGenerator.GetToken(providedEmail);
				return new ServiceResponse<AuthResponce>()
				{
					StatusCode = StatusCodes.Status200OK,
					Data = new AuthResponce(token)
				};
			}

			async Task<ServiceResponse<AuthResponce>> RegistrateWithPhoneAsync(string providedPhone, string providedPassword)
			{
				using AccountContext context = new();
				var sameUser = await context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == providedPhone);
				if (sameUser != null)
				{
					return new ServiceResponse<AuthResponce>() { StatusCode = StatusCodes.Status409Conflict };
				}
				var user = new User
				{
					PhoneNumber = providedPhone
				};
				user.HashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);
				await context.Users.AddAsync(user);
				await context.SaveChangesAsync();

				var token = jwtTokenGenerator.GetToken(providedPhone);
				return new ServiceResponse<AuthResponce>()
				{
					StatusCode = StatusCodes.Status200OK,
					Data = new AuthResponce(token)
				};
			}
		}
	}
}
