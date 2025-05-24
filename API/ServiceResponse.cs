namespace API
{
	public class ServiceResponse<T>
	{
		public int StatusCode { get; set; }
		public string ErrorMessage { get; set; }
		public T Data { get; set; }
	}
}
