using API.StorefrontService.Models;

namespace ClientAppUI.Models.Storefront
{
	public class ProductDetailViewModel
	{
		public ProductDetalisation Product { get; set; }
		public ProductDetalisation.ProductItem SelectedItem { get; set; }
		public HashSet<long> AvailableOptions { get; set; } = new();
		public HashSet<long> SelectedOptions { get; set; } = new();

		public bool IsCompleteSelection =>
			Product.Variations.Count == SelectedOptions.Count && SelectedItem != null;

		public static ProductDetailViewModel CreateFromProduct(ProductDetalisation product)
		{
			var viewModel = new ProductDetailViewModel
			{
				Product = product,
				SelectedItem =  product.ProductItems.FirstOrDefault()
			};

			foreach (var item in product.ProductItems)
			{
				foreach (var config in item.Configurations)
				{
					viewModel.AvailableOptions.Add(config.OptionId);
				}
			}

			return viewModel;
		}
	}
}
