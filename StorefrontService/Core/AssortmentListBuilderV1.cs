using API;
using API.StorefrontService;
using API.StorefrontService.Models;
using DatabaseModels.AssortmentDatabaseModels;
using DatabasesAccess.AssortmentDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorefrontService.Interfaces;

namespace StorefrontService.Core
{
	public class AssortmentListBuilderV1([FromServices] AssortmentContext context) : IAssortmentListBuilder
	{
		public async Task<ServiceResponse<AssortmentListResponce>> BuildAsync()
		{
			try
			{
				var categories = await context.Categories
				.AsNoTracking()
				.Include(c => c.Products)
					.ThenInclude(p => p.ProductItems)
				.Where(c => c.Products.Any())
				.Select(c => new AssortmentList.Category
				{
					Id = c.Id,
					Name = c.Name,
					Products = c.Products.Select(p => new AssortmentList.Product
					{
						Id = p.Id,
						Name = p.Name,
						Description = p.Description,
						ImageLink = p.Imagelink,
						Price = p.ProductItems.Count != 0 ? p.ProductItems.Min(pi => pi.Price) : 0
					}).ToList()
				})
				.ToListAsync()
				.ConfigureAwait(false);

				return ServiceResponse.Success200(new AssortmentListResponce(new AssortmentList() { Categories = categories }));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error building assortment list: {ex.Message}");
				return ServiceResponse.InternalError500<AssortmentListResponce>("Failed to retrieve assortment data");
			}
		}
	}
}
