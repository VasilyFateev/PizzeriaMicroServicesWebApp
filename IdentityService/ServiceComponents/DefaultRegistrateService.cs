using DatabaseModels.AccountDatabaseModels;
using DatabasesAccess.AccountDb;
using IdentityService.Interfaces;
using IdentityService.Utils;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.ServiceComponents
{
	internal sealed class DefaultRegistrateService : IRegistrationProvider
	{
		private readonly string providedEmailOrPhone;
		private readonly string providedPassword;

		public DefaultRegistrateService(string providedEmailOrPhone, string providedPassword)
		{
			this.providedEmailOrPhone = providedEmailOrPhone;
			this.providedPassword = providedPassword;
		}

		public async Task<IResult> RegistrateAsync()
		{
			if (string.IsNullOrEmpty(providedEmailOrPhone))
				return Results.BadRequest(providedEmailOrPhone);

			if (string.IsNullOrEmpty(providedPassword))
				return Results.BadRequest(providedPassword);

			return providedEmailOrPhone switch
			{
				string when RegexCollection.EmailRegex().IsMatch(providedEmailOrPhone) => await RegistrateWithEmailAsync(providedEmailOrPhone, providedPassword),
				string when RegexCollection.PhoneRegex().IsMatch(providedEmailOrPhone) => await RegistrateWithPhoneAsync(providedEmailOrPhone, providedPassword),
				_ => Results.BadRequest(providedEmailOrPhone)
			};
		}

		private static async Task<IResult> RegistrateWithEmailAsync(string providedEmail, string providedPassword)
		{
			using AccountContext context = new();
			var sameUser = await context.Users.FirstOrDefaultAsync(u => u.Email == providedEmail);
			if (sameUser != null)
			{
				return Results.Conflict(sameUser.Id);
			}
			var user = new User
			{
				Email = providedEmail
			};
			user.HashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);
			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();
			return Results.Ok();
		}

		private static async Task<IResult> RegistrateWithPhoneAsync(string providedPhone, string providedPassword)
		{
			using AccountContext context = new();
			var sameUser = await context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == providedPhone);
			if (sameUser != null)
			{
				return Results.Conflict(sameUser.Id);
			}
			var user = new User
			{
				PhoneNumber = providedPhone
			};
			user.HashedPassword = PasswordHasher.GetHashedPassword(user, providedPassword);
			await context.Users.AddAsync(user);
			await context.SaveChangesAsync();
			return Results.Ok();
		}
	}
}
