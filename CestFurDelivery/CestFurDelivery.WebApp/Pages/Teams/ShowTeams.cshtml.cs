using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Domain.Views;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace CestFurDelivery.WebApp.Pages.Teams
{
    public class ShowTeamsModel : PageModel
    {
		private readonly ILogger<ShowTeamsModel> _logger;
		private readonly ITeamService _teamService;

		public ShowTeamsModel(ILogger<ShowTeamsModel> logger, ITeamService teamService)
		{
			_logger = logger;
			_teamService = teamService;
		}

		public IEnumerable<Team> Teams { get; set; }

		public async Task<IActionResult> OnGetAsync(string? InputDate)
		{
			try
			{
				Teams = await _teamService.GetAll(User.Identity.Name);
				return Page();
			}
			catch (Exception ex)
			{
				_logger.LogError($"{DateTime.Now} - Index - {User.Identity.Name} - Error: {ex.Message}");
				throw;
			}
		}
	}
}
