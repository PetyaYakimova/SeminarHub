namespace SeminarHub.Models
{
	/// <summary>
	/// A category view model for when displaying categories on the seminar add or edit page. No added validations.
	/// </summary>
	public class CategoryViewModel
	{
		/// <summary>
		/// Category Identifier
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Category Name
		/// </summary>
		public string Name { get; set; } = string.Empty;
	}
}
