﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Data
{
	[Comment("Seminars")]
	public class Seminar
	{
		[Key]
		[Comment("Seminar Identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(SeminarTopicMaxLength)]
		[Comment("Seminar Topic")]
		public string Topic { get; set; } = string.Empty;

		[Required]
		[MaxLength(SeminarLecturerMaxLength)]
		[Comment("Seminar Lecturer")]
		public string Lecturer { get; set; } = string.Empty;

		[Required]
		[MaxLength(SeminarDetailsMaxLength)]
		[Comment("Seminar Details")]
		public string Details { get; set; } = string.Empty;

		[Required]
		[Comment("Seminar Organizer")]
		public string OrganizerId { get; set; } = string.Empty;

		[ForeignKey(nameof(OrganizerId))]
		public IdentityUser Organizer { get; set; } = null!;

		[Required]
		[Comment("Seminar Date and Time")]
		public DateTime DateAndTime { get; set; }

		[Range(SeminarDurationMinValue, SeminarDurationMaxValue)]
		[Comment("Seminar Duration")]
		public int? Duration { get; set; }

		[Required]
		[Comment("Seminar Category")]
		public int CategoryId { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; } = null!;

		public IList<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
	}
}
