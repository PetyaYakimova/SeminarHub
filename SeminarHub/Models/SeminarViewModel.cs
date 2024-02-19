namespace SeminarHub.Models
{
	/// <summary>
	/// A seminar view model for only displaying the seminar data when viwing collection of seminars. No added validations.
	/// </summary>
	public class SeminarViewModel
	{
		/// <summary>
		/// Seminar Identifier
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Seminar Topic
		/// </summary>
		public string Topic { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Lecturer
		/// </summary>
		public string Lecturer { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Category Name
		/// </summary>
		public string Category { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Date and Time
		/// </summary>
		public string DateAndTime { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Organizer's Name
		/// </summary>
		public string Organizer { get; set; } = string.Empty;
	}
}
