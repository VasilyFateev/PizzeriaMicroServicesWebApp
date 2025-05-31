using API;
using API.StorefrontService.Models;
using DatabasesAccess.AssortmentDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorefrontService.Interfaces;

namespace StorefrontService.Core
{
	public class ProductItemConfiguratorBuilderV1([FromServices] AssortmentContext context) : IProductItemConfiguratorBuilder
	{
		public async Task<ServiceResponse<ProductItemConfigurator>> BuildAsync(long productId)
		{
			var product = await context.Products
				.Where(p => p.Id == productId)
				.Include(p => p.ProductItems)
					.ThenInclude(pi => pi.Configurations)
				.Include(p => p.Category)
					.ThenInclude(c => c.Variations)
						.ThenInclude(v => v.VariationOptions)
				.FirstOrDefaultAsync();
			if (product == null)
			{
				return new ServiceResponse<ProductItemConfigurator>(404, string.Empty, null);
			}

			var result = new ProductItemConfigurator()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				ProductItems = [.. product.ProductItems
					.Select(pi => new ProductItemConfigurator.ProductItem(pi.Id, pi.Price, pi.Imagelink,
					[.. pi.Configurations.Select(pc => new ProductItemConfigurator.Configuration(pc.ProductItemId, pc.VariationOptionId))]
					))],
				Variations = [.. product.Category.Variations
					.Select(v => new ProductItemConfigurator.Variation(v.Id, v.Name,
					[.. v.VariationOptions.Select(vo => new ProductItemConfigurator.Option(vo.Id, vo.Name))]
					))],
			};
			return new ServiceResponse<ProductItemConfigurator>(200, string.Empty, result);
		}
	}
}
