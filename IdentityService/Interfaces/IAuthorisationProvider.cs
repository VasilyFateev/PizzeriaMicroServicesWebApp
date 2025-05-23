namespace IdentityService.Interfaces
{
	internal interface IAuthorisationProvider
	{
		public Task<IResult> AuthorizeAsync();
	}
}
