namespace SeminarHub.Models
{
	/// <summary>
	/// A seminar view model for only displaying the seminar data when previweing it before deletion. No added validations.
	/// </summary>
	public class SeminarDeleteViewModel
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
		/// Seminar Date and Time
		/// </summary>
		public DateTime DateAndTime { get; set; } 
	}
}
