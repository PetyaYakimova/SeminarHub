namespace SeminarHub.Data
{
	public static class DataConstants
	{
		public const int SeminarTopicMaxLength = 100;
		public const int SeminarTopicMinLength = 3;

		public const int SeminarLecturerMaxLength = 60;
		public const int SeminarLecturerMinLength = 5;

		public const int SeminarDetailsMaxLength = 500;
		public const int SeminarDetailsMinLength = 10;

		public const int SeminarDurationMaxValue = 180;
		public const int SeminarDurationMinValue = 30;

		public const int CategoryNameMaxLength = 50;
		public const int CategoryNameMinLength = 3;

		public const string DateTimeFormat = "dd/MM/yyyy HH:mm";

		public const string RequiredFieldErrorMessage = "The field {0} is required!";
		public const string StringLengthErrorMessage = "The field {0} must be between {2} and {1} characters long!";
		public const string IntegerRangeErrorMessage = "The field {0} must be between {1} and {2}!";
		public const string InvalidDateFormatErrorMessage = "Invalid date!";
	}
}
