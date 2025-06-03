namespace API.StorefrontService.Models
{
	public class AssortmentList
	{
		public List<Category> Categories { get; set; } = [];
		public class Category
		{
			public long Id { get; set; }
			public string Name { get; set; }
			public List<Product> Products { get; set; } = [];
		}
		public class Product
		{
			public long Id { get; set; }
			public string Name { get; set; }
			public string? Description { get; set; }
			public string? ImageLink { get; set; }
			public decimal? Price { get; set; }
		}
	}
}
