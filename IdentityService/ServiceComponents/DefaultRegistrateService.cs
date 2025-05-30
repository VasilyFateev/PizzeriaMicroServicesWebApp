using API;
using API.ClientWebAppIdentityService;
using DatabaseModels.AccountDatabaseModels;
using DatabasesAccess.AccountDb;
using IdentityService.Interfaces;
using IdentityService.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.ServiceComponents
{
	internal sealed class DefaultRegistrateService(IJwtTokenGenerator jwtTokenGenerator, [FromServices] AccountContext accountContext) : IRegistrationProvider
	{
		public async Task<ServiceResponse<RegResponce>> RegistrateAsync(string providedName, string providedLogin, string providedPassword)
		{
			if (string.IsNullOrEmpty(providedName))
				return new ServiceResponse<RegResponce>(StatusCodes.Status400BadRequest, string.Empty, null);

			if (string.IsNullOrEmpty(providedLogin))
				return new ServiceResponse<RegResponce>(StatusCodes.Status400BadRequest, string.Empty, null);

			if (string.IsNullOrEmpty(providedPassword))
				return new ServiceResponse<RegResponce>(StatusCodes.Status400BadRequest, string.Empty, null);

			var user = providedLogin switch
			{
				string when RegexCollection.EmailRegex().IsMatch(providedLogin) => await CreateUserWithEmailAsync(providedLogin),
				string when RegexCollection.PhoneRegex().IsMatch(providedLogin) => await CreateUserWithPhoneNumberAsync(providedLogin),
				_ => null
			};

			if (user == null)
			{
				return new ServiceResponse<RegResponce>(StatusCodes.Status409Conflict, string.Empty, null);
			}
			user.HashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);

			try
			{
				await accountContext.Users.AddAsync(user);
				await accountContext.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				if (ex.InnerException != null)
					Console.WriteLine(ex.InnerException.Message);
				throw;
			}

			var token = jwtTokenGenerator.GetToken(providedLogin);
			return new ServiceResponse<RegResponce>(StatusCodes.Status200OK, string.Empty, new RegResponce(providedName, token));

			async Task<User?> CreateUserWithEmailAsync(string providedEmail)
			{
				var sameUser = await accountContext.Users.FirstOrDefaultAsync(u => u.Email == providedEmail);
				if (sameUser != null)
				{
					return null;
				}
				var user = new User
				{
					Email = providedEmail,
					Name = providedName
				};
				return user;
			}
			async Task<User?> CreateUserWithPhoneNumberAsync(string providedPhone)
			{
				var sameUser = await accountContext.Users.FirstOrDefaultAsync(u => u.PhoneNumber == providedPhone);
				if (sameUser != null)
				{
					return null;
				}
				var user = new User
				{
					PhoneNumber = providedPhone,
					Name = providedName
				};
				return user;
			}
		}
	}
}
