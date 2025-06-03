using API;
using API.StorefrontService;
using API.StorefrontService.Models;
using DatabasesAccess.AssortmentDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorefrontService.Interfaces;

namespace StorefrontService.Core
{
	public class ProductDetalisationBuilderV1([FromServices] AssortmentContext context) : IProductDetalisationBuilder
	{
		public async Task<ServiceResponse<ProductDetalisationResponce>> BuildAsync(long productId)
		{
			var product = await context.Products
				.AsNoTracking()
				.Where(p => p.Id == productId)
				.Include(p => p.ProductItems)
					.ThenInclude(pi => pi.Configurations)
				.Include(p => p.Category)
					.ThenInclude(c => c.Variations)
						.ThenInclude(v => v.VariationOptions)
				.FirstOrDefaultAsync();

			if (product == null)
			{
				return ServiceResponse.NoFund404<ProductDetalisationResponce>();
			}

			var result = new ProductDetalisation()
			{
				Id = product.Id,
				Name = product.Name,
				Description = product.Description,
				ProductItems = [.. product.ProductItems
					.Select(pi => new ProductDetalisation.ProductItem(pi.Id, pi.Price, pi.Imagelink,
					[.. pi.Configurations.Select(pc => new ProductDetalisation.Configuration(pc.ProductItemId, pc.VariationOptionId))]
					))],
				Variations = [.. product.Category.Variations
					.Select(v => new ProductDetalisation.Variation(v.Id, v.Name,
					[.. v.VariationOptions.Select(vo => new ProductDetalisation.Option(vo.Id, vo.Name))]
					))],
			};
			if (result == null)
			{
				return ServiceResponse.NoFund404<ProductDetalisationResponce>();
			}
			return ServiceResponse.Success200(new ProductDetalisationResponce(result));
		}
	}
}