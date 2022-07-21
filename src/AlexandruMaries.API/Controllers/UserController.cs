using AlexandruMaries.API.Model;
using AlexandruMaries.Data.Interfaces;
using AlexandruMaries.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlexandruMaries.API.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UserController : Controller
	{
		private readonly IUserRepository userRepository;

		public UserController(IUserRepository userRepository)
		{
			this.userRepository = userRepository;
		}

		[Authorize]
		[HttpPost("register")]
		public async Task<ActionResult<int>> Register([FromBody] UserRequest userRegisterRequest)
		{
			var response = userRepository.Register(
				userRegisterRequest.UserName,
				userRegisterRequest.Password);

			if(!response.IsCompletedSuccessfully)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}

		[HttpPost("login")]
		public async Task<ActionResult<string>> Login([FromBody] UserRequest userRegisterRequest)
		{
			var response = userRepository.Login(
				userRegisterRequest.UserName,
				userRegisterRequest.Password);

			if (!response.IsCompletedSuccessfully)
			{
				return BadRequest(response);
			}

			return Ok(response);
		}


	}
}
