namespace API.StorefrontService.Models
{
	public class AssortmentList
	{
		public List<Category> Categories { get; set; } = [];

		public record Category(long Id, string Name, List<Product> Products);
		public record Product(long Id, string Name, string? Description, string? ImageLink, decimal? Price);
	}
}
