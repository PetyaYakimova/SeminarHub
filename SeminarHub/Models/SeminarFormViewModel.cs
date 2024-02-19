using System.ComponentModel.DataAnnotations;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Models
{
	/// <summary>
	/// A seminar view model for the seminar forms. Added validations.
	/// </summary>
	public class SeminarFormViewModel
	{
		/// <summary>
		/// Seminar Topic
		/// </summary>
		[Required(ErrorMessage = RequiredFieldErrorMessage)]
		[StringLength(SeminarTopicMaxLength, MinimumLength = SeminarTopicMinLength, ErrorMessage = StringLengthErrorMessage)]
		public string Topic { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Lecturer
		/// </summary>
		[Required(ErrorMessage = RequiredFieldErrorMessage)]
		[StringLength(SeminarLecturerMaxLength, MinimumLength = SeminarLecturerMinLength, ErrorMessage = StringLengthErrorMessage)]
		public string Lecturer { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Details
		/// </summary>
		[Required(ErrorMessage = RequiredFieldErrorMessage)]
		[StringLength(SeminarDetailsMaxLength, MinimumLength = SeminarDetailsMinLength, ErrorMessage = StringLengthErrorMessage)]
		public string Details { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Date and Time
		/// </summary>
		[Required(ErrorMessage = RequiredFieldErrorMessage)]
		public string DateAndTime { get; set; } = string.Empty;

		/// <summary>
		/// Seminar Duration
		/// </summary>
		[Range(SeminarDurationMinValue, SeminarDurationMaxValue, ErrorMessage = IntegerRangeErrorMessage)]
		public int? Duration { get; set; }

		/// <summary>
		/// Seminar Category Identifier
		/// </summary>
		[Required(ErrorMessage = RequiredFieldErrorMessage)]
		public int CategoryId { get; set; }

		/// <summary>
		/// All posible categories
		/// </summary>
		public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
	}
}
