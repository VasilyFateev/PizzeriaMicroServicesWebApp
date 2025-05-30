namespace API.ClientWebAppIdentityService
{
	public record AuthRequest(string Login, string Password);
	public record RegRequest(string Name, string Login, string Password);
	public record PingRequest();
}
