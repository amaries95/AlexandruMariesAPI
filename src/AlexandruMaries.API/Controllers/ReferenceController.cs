using AlexandruMaries.Data.RepoModels;
using AlexandruMaries.Infrastructure.Interfaces;
using AlexandruMaries.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AlexandruMaries.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ReferenceController : Controller
	{
		private readonly IReferenceRepository _referenceRepository;

		public ReferenceController(IReferenceRepository referenceRepository)
		{
			_referenceRepository = referenceRepository;
		}

		[HttpGet("/[controller]/visible")]
		public async Task<IActionResult> GetVisibleReferences()
		{
			var response = _referenceRepository.GetAllReferences(true);

			return Ok(response);
		}

		[HttpPost("/[controller]/add")]
		public async Task<IActionResult> AddNewReference([FromBody] ReferenceRequest referenceRequest)
		{
			var response = await _referenceRepository.AddNewReference(referenceRequest);

			return Ok(response);
		}

		[Authorize]
		[HttpGet("/[controller]/all")]
		public async Task<IActionResult> GetAllReferences()
		{
			var response = _referenceRepository.GetAllReferences(false);

			return Ok(response);
		}

		[Authorize]
		[HttpDelete("/[controller]/delete/{id}")]
		public async Task<IActionResult> DeleteReference(int id)
		{
			var response = await _referenceRepository.DeleteReference(id);

			return Ok(response);
		}

		[Authorize]
		[HttpPatch("/[controller]/update")]
		public async Task<IActionResult> UpdateVisibilityForReferences([FromBody] ReferenceRequest reference)
		{
			await _referenceRepository.UpdateVisibilityOnReferences(reference);

			return Ok();
		}
	}
}
