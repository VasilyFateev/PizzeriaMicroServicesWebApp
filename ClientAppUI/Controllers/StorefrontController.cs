using ClientAppUI.Models.Storefront;
using ClientAppUI.RequestSenders.StorefrontService;
using Microsoft.AspNetCore.Mvc;

namespace ClientAppUI.Controllers
{
	public class StorefrontController : Controller
	{
		[HttpGet]
		public async Task<IActionResult> GetAssortmentList([FromServices] AssortmentListRequestSenderService senderService)
		{
			var responce = await senderService.SendMessageAsync();
			if (responce == null)
			{
				return StatusCode(500, "Internal Server Error");
			}
			switch (responce.StatusCode)
			{
				case StatusCodes.Status200OK:
					if (responce.Data == null)
					{
						return StatusCode(500, "Internal Server Error");
					}
					return View("AssortmentList", responce.Data.Assortment);
				default:
					return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpGet]
		public async Task<IActionResult> GetProductDetalisation(long id, [FromServices] ProductDetalisationRequestSenderService senderService)
		{
			var response = await senderService.SendMessageAsync(id);
			if (response == null)
			{
				return StatusCode(500, "Internal Server Error");
			}

			return response.StatusCode switch
			{
				StatusCodes.Status200OK => response.Data != null && response.Data.ProductDetalisation != null
					? PartialView("_ProductModal", ProductDetailViewModel.CreateFromProduct(response.Data.ProductDetalisation))
					: StatusCode(500, "Internal Server Error"),
				_ => StatusCode(response.StatusCode, response.ErrorMessage)
			};
		}
	}
}
