using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace AlexandruMaries.API.Model
{
	public class UserRequest
	{
		[JsonPropertyName("username")]
		public string UserName { get; set; }

		[JsonPropertyName("password")]
		public string Password { get; set; }
	}
}
