using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using CestFurDelivery.Services.Services;
using CestFurDelivery.WebApp.Pages.Deliveries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace CestFurDelivery.WebApp.Pages.Teams
{
    public class UpdateTeamsModel : PageModel
    {
		private readonly ITeamService _teamService;
		private readonly ILogger<UpdateTeamsModel> _logger;

		public UpdateTeamsModel(ITeamService teamService, ILogger<UpdateTeamsModel> logger)
		{
			_teamService = teamService;
			_logger = logger;
		}

		[BindProperty]
		public Team Team { get; set; }

		public async Task<IActionResult> OnGetAsync(string Id)
		{
			try
			{
				Team = await _teamService.GetById(Guid.Parse(Id), User.Identity.Name);
				if (Team == null)
					return NotFound();
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{DateTime.Now} - UpdateTeams - {User.Identity.Name} - Error: {ex.Message}");
				return RedirectToPage("/Index");
			}
		}

		public async Task<IActionResult> OnPostAsync(string Id)
		{
			try
			{
				Team.Id = Guid.Parse(Id);
				if (ModelState.IsValid)
				{
					await _teamService.Update(Team, User.Identity.Name);
					return RedirectToPage("/Teams/ShowTeams");
				}

				Team = await _teamService.GetById((Guid)Team.Id, User.Identity.Name);
				if (Team == null)
					return NotFound();
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{DateTime.Now} - UpdateDelivery - {User.Identity.Name} - Error: {ex.Message}");
				throw;
			}
		}
	}
}
