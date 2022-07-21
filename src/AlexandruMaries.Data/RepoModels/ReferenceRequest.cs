using System.Text.Json.Serialization;

namespace AlexandruMaries.Data.RepoModels
{
	public class ReferenceRequest
	{
		[JsonPropertyName("id")]
		public int? Id { get; set; }

		[JsonPropertyName("summary")]
		public string? Summary { get; set; }

		[JsonPropertyName("author")]
		public string? Author { get; set; }

		[JsonPropertyName("jobTitleAuthor")]
		public string? JobTitleAuthor { get; set; }

		[JsonPropertyName("isVisible")]
		public bool IsVisible { get; set; } = true;

	}
}
