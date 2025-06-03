namespace API.StorefrontService.Models
{
	public class ProductDetalisation
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public List<ProductItem> ProductItems { get; set; } = [];
		public List<Variation> Variations { get; set; } = [];

		public record ProductItem(long Id, decimal? Price, string? ImageLink, List<Configuration> Configurations);
		public record Configuration(long ItemId, long OptionId);
		public record Variation(long Id, string Name, List<Option> Options);
		public record Option(long Id, string Name);
	}
}
