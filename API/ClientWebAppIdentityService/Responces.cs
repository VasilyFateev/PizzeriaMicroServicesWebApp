namespace API.ClientWebAppIdentityService
{
	public record AuthResponce(string Name, string JwtToken);
	public record RegResponce(string Name, string JwtToken);
	public record PingResponce();
}
