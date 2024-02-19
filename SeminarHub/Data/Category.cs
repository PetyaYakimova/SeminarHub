using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data
{
	[Comment("Categories")]
	public class Category
	{
		[Key]
		[Comment("Category Identifierr")]
		public int Id { get; set; }

		[Required]
		[MaxLength(CategoryNameMaxLength)]
		[Comment("Category Name")]
		public string Name { get; set; } = string.Empty;

		public IList<Seminar> Seminars { get; set; } = new List<Seminar>();
	}
}