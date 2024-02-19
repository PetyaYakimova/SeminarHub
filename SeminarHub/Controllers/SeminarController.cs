using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeminarHub.Data;
using SeminarHub.Models;
using System.Globalization;
using System.Security.Claims;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Controllers
{
	[Authorize]
	public class SeminarController : Controller
	{
		private readonly SeminarHubDbContext data;

		public SeminarController(SeminarHubDbContext context)
		{
			this.data = context;
		}

		[HttpGet]
		public async Task<IActionResult> Add()
		{
			var model = new SeminarFormViewModel();
			model.Categories = await GetCategories();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(SeminarFormViewModel model)
		{
			DateTime seminarDate = DateTime.Now;

			if (!DateTime.TryParseExact(
				model.DateAndTime,
				DateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out seminarDate))
			{
				ModelState.AddModelError(nameof(model.DateAndTime), InvalidDateFormatErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await GetCategories();

				return View(model);
			}

			var entity = new Seminar()
			{
				Topic = model.Topic,
				Lecturer = model.Lecturer,
				Details = model.Details,
				DateAndTime = seminarDate,
				Duration = model.Duration,
				CategoryId = model.CategoryId,
				OrganizerId = GetUserId()
			};

			await data.Seminars.AddAsync(entity);
			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			var seminars = await data.Seminars
				.AsNoTracking()
				.Select(s => new SeminarViewModel
				{
					Id = s.Id,
					Topic = s.Topic,
					Lecturer = s.Lecturer,
					Category = s.Category.Name,
					DateAndTime = s.DateAndTime.ToString(DateTimeFormat),
					Organizer = s.Organizer.UserName
				})
				.ToListAsync();

			return View(seminars);
		}

		[HttpGet]
		public async Task<IActionResult> Joined()
		{
			var seminars = await data.SeminarsParticipants
				.Include(sp => sp.Seminar)
				.AsNoTracking()
				.Where(sp => sp.ParticipantId == GetUserId())
				.Select(sp => new SeminarViewModel
				{
					Id = sp.SeminarId,
					Topic = sp.Seminar.Topic,
					Lecturer = sp.Seminar.Lecturer,
					Category = sp.Seminar.Category.Name,
					DateAndTime = sp.Seminar.DateAndTime.ToString(DateTimeFormat),
					Organizer = sp.Seminar.Organizer.UserName
				})
				.ToListAsync();

			return View(seminars);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var seminar = await data.Seminars
				.FindAsync(id);

			if (seminar == null)
			{
				return BadRequest();
			}

			if (seminar.OrganizerId != GetUserId())
			{
				return Unauthorized();
			}

			var model = new SeminarFormViewModel()
			{
				Topic = seminar.Topic,
				Lecturer = seminar.Lecturer,
				Details = seminar.Details,
				DateAndTime = seminar.DateAndTime.ToString(DateTimeFormat),
				Duration = seminar.Duration,
				CategoryId = seminar.CategoryId
			};

			model.Categories = await GetCategories();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(SeminarFormViewModel model, int id)
		{
			var seminar = await data.Seminars
				.FindAsync(id);

			if (seminar == null)
			{
				return BadRequest();
			}

			if (seminar.OrganizerId != GetUserId())
			{
				return Unauthorized();
			}

			DateTime dateTime = DateTime.Now;

			if (!DateTime.TryParseExact(
				model.DateAndTime,
				DateTimeFormat,
				CultureInfo.InvariantCulture,
				DateTimeStyles.None,
				out dateTime))
			{
				ModelState.AddModelError(nameof(model.DateAndTime), InvalidDateFormatErrorMessage);
			}

			if (!ModelState.IsValid)
			{
				model.Categories = await GetCategories();

				return View(model);
			}

			seminar.Topic = model.Topic;
			seminar.Lecturer = model.Lecturer;
			seminar.Details = model.Details;
			seminar.DateAndTime = dateTime;
			seminar.Duration = model.Duration;
			seminar.CategoryId = model.CategoryId;

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		[HttpPost]
		public async Task<IActionResult> Join(int id)
		{
			var seminar = await data.Seminars
				.Where(s => s.Id == id)
				.Include(s => s.SeminarsParticipants)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			if (seminar.OrganizerId == userId)
			{
				return Unauthorized();
			}

			if (seminar.SeminarsParticipants.Any(sp => sp.ParticipantId == userId))
			{
				return RedirectToAction(nameof(All));
			}

			seminar.SeminarsParticipants.Add(new SeminarParticipant()
			{
				SeminarId = seminar.Id,
				ParticipantId = userId
			});

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(Joined));
		}

		[HttpPost]
		public async Task<IActionResult> Leave(int id)
		{
			var seminar = await data.Seminars
				.Where(s => s.Id == id)
				.Include(s => s.SeminarsParticipants)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			string userId = GetUserId();

			var seminarParticipant = seminar.SeminarsParticipants.FirstOrDefault(sp => sp.ParticipantId == userId);

			if (seminarParticipant == null)
			{
				return BadRequest();
			}

			seminar.SeminarsParticipants.Remove(seminarParticipant);

			await data.SaveChangesAsync();

			return RedirectToAction(nameof(Joined));
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			var seminar = await data.Seminars
				.AsNoTracking()
				.Where(s => s.Id == id)
				.Include(s => s.Category)
				.Include(s => s.Organizer)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			var model = new SeminarDetailsViewModel()
			{
				Id = seminar.Id,
				Topic = seminar.Topic,
				Lecturer = seminar.Lecturer,
				Details = seminar.Details,
				DateAndTime = seminar.DateAndTime.ToString(DateTimeFormat),
				Duration = seminar.Duration,
				Category = seminar.Category.Name,
				Organizer = seminar.Organizer.UserName
			};

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var seminar = await data.Seminars
				.FindAsync(id);

			if (seminar == null)
			{
				return BadRequest();
			}

			if (seminar.OrganizerId != GetUserId())
			{
				return Unauthorized();
			}

			var model = new SeminarDeleteViewModel()
			{
				Id = id,
				Topic = seminar.Topic,
				DateAndTime = seminar.DateAndTime
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed( int id)
		{
			var seminar = await data.Seminars
				.Where(s=>s.Id==id)
				.Include(s=>s.SeminarsParticipants)
				.FirstOrDefaultAsync();

			if (seminar == null)
			{
				return BadRequest();
			}

			if (seminar.OrganizerId != GetUserId())
			{
				return Unauthorized();
			}

			if (seminar.SeminarsParticipants.Any())
			{
				return BadRequest();
			}

			data.Remove(seminar);
			await data.SaveChangesAsync();

			return RedirectToAction(nameof(All));
		}

		/// <summary>
		/// Get the current user id.
		/// </summary>
		/// <returns></returns>
		private string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}

		/// <summary>
		/// Get all currently available categories.
		/// </summary>
		/// <returns></returns>
		private async Task<IEnumerable<CategoryViewModel>> GetCategories()
		{
			return await data.Categories
				.AsNoTracking()
				.Select(e => new CategoryViewModel
				{
					Id = e.Id,
					Name = e.Name
				}).ToListAsync();
		}
	}
}
