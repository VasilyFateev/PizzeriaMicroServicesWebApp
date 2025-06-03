namespace API
{
	public record ServiceResponse<T>(int StatusCode, string ErrorMessage, T? Data) where T : class;

	public static class ServiceResponse
	{
		public static ServiceResponse<T> Success200<T>(T result, string message = "") where T : class
		{
			return new(200, message, result);
		}

		public static ServiceResponse<T> Success200<T>(string message = "") where T : class
		{
			return new(200, message, null);
		}

		public static ServiceResponse<T> BadRequest400<T>(string message = "Not Found") where T : class
		{
			return new(400, message, null);
		}

		public static ServiceResponse<T> Unauthorized401<T>(string message = "Unauthorized") where T : class
		{
			return new(401, message, null);
		}

		public static ServiceResponse<T> NoFund404<T>(string message = "Bad Request") where T : class
		{
			return new(404, message, null);
		}

		public static ServiceResponse<T> Conflict409<T>(string message = "Conflict") where T : class
		{
			return new(409, message, null);
		}

		public static ServiceResponse<T> InternalError500<T>(string message = "Internal Server Error") where T : class
		{
			return new(500, message, null);
		}
	}
}
