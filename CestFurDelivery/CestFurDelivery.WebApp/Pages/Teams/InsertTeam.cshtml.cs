using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CestFurDelivery.WebApp.Pages.Teams
{
    public class InsertTeamModel : PageModel
    {
		private readonly ITeamService _teamService;
		private readonly ILogger<InsertTeamModel> _logger;

		public InsertTeamModel(ITeamService teamService, ILogger<InsertTeamModel> logger)
		{
			_teamService = teamService;
			_logger = logger;
		}

		[BindProperty]
		public Team Team { get; set; }

		public IActionResult OnGetAsync()
		{
			try
			{
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{DateTime.Now} - UpdateTeams - {User.Identity.Name} - Error: {ex.Message}");
				return RedirectToPage("/Index");
			}
		}

		public async Task<IActionResult> OnPostAsync()
		{
			try
			{
				if (ModelState.IsValid)
				{
					Team.Id= Guid.NewGuid();
					Team.IsActive = false;
					await _teamService.Insert(Team, User.Identity.Name);
					return RedirectToPage("/Teams/ShowTeams");
				}
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
