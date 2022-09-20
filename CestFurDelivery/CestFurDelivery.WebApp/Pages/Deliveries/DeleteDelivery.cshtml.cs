using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CestFurDelivery.WebApp.Pages.Deliveries
{
    [Authorize(Roles = "Admin,ReadWrite")]
    public class DeleteDeliveryModel : PageModel
    {
        private readonly ILogger<DeleteDeliveryModel> _logger;
        private readonly IDeliveryService _deliveryService;
        private readonly IDeliveryStateService _deliveryStateService;
        private readonly ITeamService _teamService;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DeleteDeliveryModel(ILogger<DeleteDeliveryModel> logger,
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            IDeliveryService deliveryService,
            IDeliveryStateService deliveryStateService,
            ITeamService teamService)
        {
            _logger = logger;
            _deliveryService = deliveryService;
            _deliveryStateService = deliveryStateService;
            _teamService = teamService;
        }

        public Delivery Delivery { get; set; }
        public Team Team { get; set; }
        public DeliveryState DeliveryState { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            try
            {
                Delivery = await _deliveryService.GetById(Guid.Parse(Id), User.Identity.Name);
                if (Delivery == null)
                    return NotFound();

                Team = await _teamService.GetById(Delivery.Team, User.Identity.Name);
                DeliveryState = await _deliveryStateService.GetById(Delivery.IdDeliveryState, User.Identity.Name);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - UpdateDelivery - {User.Identity.Name} - Error: {ex.Message}");
                return RedirectToPage("/Index");
            }
        }
        public async Task<IActionResult> OnPostAsync(string Id)
        {
            try
            {
                Guid id;
                if (Guid.TryParse(Id, out id))
                {
                    Delivery = await _deliveryService.GetById(id, User.Identity.Name);
                    if (Delivery != null)
                    {
                        await _deliveryService.Delete(id, User.Identity.Name);
                        return RedirectToPage("/Index", new { InputDate = Delivery.Date.ToString("dd-MM-yyyy") });
                    }
                }
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - UpdateDelivery - {User.Identity.Name} - Error: {ex.Message}");
                return RedirectToPage("/Index");
            }
        }
    }
}
