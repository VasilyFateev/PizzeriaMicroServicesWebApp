using API;
using API.StorefrontService.Models;
using DatabasesAccess.AssortmentDb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorefrontService.Interfaces;

namespace StorefrontService.Core
{
	public class AssortmentListBuilderV1([FromServices] AssortmentContext context) : IAssortmentListBuilder
	{
		public async Task<ServiceResponse<AssortmentList>> BuildAsync()
		{
			var allData = context.Categories
				.Include(c => c.Products);

			var result = new AssortmentList()
			{
				Categories = await allData.Select(c => new AssortmentList.Category(c.Id, c.Name,
					c.Products.Select(p => new AssortmentList.Product(p.Id, p.Name, p.Description, p.Imagelink,
						p.ProductItems.Min(pi => pi.Price)))
					.ToList()
				)).ToListAsync()
			};

			return new ServiceResponse<AssortmentList>(200, string.Empty, result);			
		}
	}
}
