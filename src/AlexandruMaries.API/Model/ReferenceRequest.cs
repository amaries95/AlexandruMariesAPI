using Microsoft.AspNetCore.Mvc;

namespace AlexandruMaries.API.Model
{
	public class ReferenceRequest
	{
		public int Id { get; set; }

		public string Summary { get; set; }

		public string Author { get; set; }

		public string JobTitleAuthor { get; set; }

		public bool IsVisible { get; set; }
	}
}
