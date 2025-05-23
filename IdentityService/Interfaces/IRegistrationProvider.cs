namespace IdentityService.Interfaces
{
	internal interface IRegistrationProvider
	{
		public Task<IResult> RegistrateAsync();
	}
}
