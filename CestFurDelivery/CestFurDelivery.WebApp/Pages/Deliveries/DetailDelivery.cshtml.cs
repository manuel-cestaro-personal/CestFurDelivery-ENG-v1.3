using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CestFurDelivery.WebApp.Pages.Deliveries
{
    [Authorize]
    public class DetailDeliveryModel : PageModel
    {
        private readonly ILogger<DetailDeliveryModel> _logger;
        private readonly IDeliveryService _deliveryService;
        private readonly IDeliveryStateService _deliveryStateService;
        private readonly ITeamService _teamService;
        private readonly IVehicleService _vehicleService;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public DetailDeliveryModel(ILogger<DetailDeliveryModel> logger,
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            IDeliveryService deliveryService,
            IDeliveryStateService deliveryStateService,
            ITeamService teamService,
            IVehicleService vehicleService)
        {
            _logger = logger;
            _deliveryService = deliveryService;
            _deliveryStateService = deliveryStateService;
            _teamService = teamService;
            _vehicleService = vehicleService;
        }

        public Delivery Delivery { get; set; }
        public Team Team { get; set; }
        public Vehicle Vehicle1 { get; set; }
        public Vehicle? Vehicle2 { get; set; }
        public Vehicle? Vehicle3 { get; set; }
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
                Vehicle1 = await _vehicleService.GetById(Delivery.Vehicle1, User.Identity.Name);
                if (Delivery.Vehicle2 != null)
                {
                    Vehicle2 = await _vehicleService.GetById((Guid)Delivery.Vehicle2, User.Identity.Name);
                }
                if (Delivery.Vehicle3 != null)
                {
                    Vehicle3 = await _vehicleService.GetById((Guid)Delivery.Vehicle3, User.Identity.Name);
                }
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - DetailDelivery - {User.Identity.Name} - Error: {ex.Message}");
                return RedirectToPage("/Index");
            }
        }
    }
}
