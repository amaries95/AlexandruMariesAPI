using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AlexandruMaries.Model
{
	public class Reference
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string Summary { get; set; }

		public string Author { get; set; }

		public string JobTitleAuthor { get; set; }

		public bool IsVisible { get; set; } = true;
	}
}