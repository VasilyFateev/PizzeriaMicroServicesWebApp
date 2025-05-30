namespace API
{
	public record ServiceResponse<T>(int StatusCode, string ErrorMessage, T? Data) where T : class;
}
