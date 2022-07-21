using AlexandruMaries.Data.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexandruMaries.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ViewsController : Controller
	{
		private readonly IViewsRepository viewsRepository;

		public ViewsController(IViewsRepository viewsRepository)
		{
			this.viewsRepository = viewsRepository;
		}

		[HttpPut("/[controller]/increment")]
		public async Task<IActionResult> AddViews()
		{
			await viewsRepository.UpdateViews();

			return Ok();
		}

		[Authorize]
		[HttpGet("/[controller]/all")]
		public async Task<IActionResult> GetViews()
		{
			var response = viewsRepository.GetViews();
			return Ok(response.Result);
		}
	}
}
