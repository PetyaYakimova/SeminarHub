using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data
{
	[Comment("Seminars Participants")]
	public class SeminarParticipant
	{
		[Required]
		[Comment("Seminar Identifier")]
		public int SeminarId { get; set; }

		[ForeignKey(nameof(SeminarId))]
		public Seminar Seminar { get; set; } = null!;

		[Required]
		[Comment("Participant Identifier")]
		public string ParticipantId { get; set; } = string.Empty;

		[ForeignKey(nameof(ParticipantId))]
		public IdentityUser Participant { get; set; } = null!;
	}
}