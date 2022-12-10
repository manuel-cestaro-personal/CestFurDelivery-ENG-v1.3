using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CestFurDelivery.WebApp.Pages.Teams
{
    public class ChangeStatusTeamModel : PageModel
    {
        private readonly ILogger<ChangeStatusTeamModel> _logger;
        private readonly ITeamService _teamService;

        public ChangeStatusTeamModel(ILogger<ChangeStatusTeamModel> logger, ITeamService teamService)
        {
            _logger = logger;
            _teamService = teamService;
        }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            try
            {
                Guid TeamId = Guid.Parse(Id);
                await _teamService.ChangeSet(TeamId, User.Identity.Name);
                _logger.LogInformation($"{DateTime.Now} - ChangeStatusTeam - {User.Identity.Name} - Update status complete");
                return RedirectToPage("/Teams/ShowTeams");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - ChangeStatusTeam - {User.Identity.Name} - Error: {ex.Message}");
                return RedirectToPage("/Index");
            }
        }
    }
}
