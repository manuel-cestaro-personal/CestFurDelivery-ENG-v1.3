using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CestFurDelivery.WebApp.Pages.Deliveries
{
    [Authorize(Roles = "Admin,ReadWrite")]
    public class ChangeStatusDeliveryModel : PageModel
    {
        private readonly ILogger<ChangeStatusDeliveryModel> _logger;
        private readonly IDeliveryService _deliveryService;
        private readonly IChangeStatusService _changeStatusService;

        public ChangeStatusDeliveryModel(ILogger<ChangeStatusDeliveryModel> logger,
            IDeliveryService deliveryService,
            IChangeStatusService changeStatusService)
        {
            _logger = logger;
            _deliveryService = deliveryService;
            _changeStatusService = changeStatusService;
        }

        public Delivery Delivery { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            try
            {
                Delivery = await _deliveryService.GetById(Guid.Parse(Id), User.Identity.Name);
                if (Delivery == null)
                    return NotFound();
                Guid newState = await _changeStatusService.FindStatus(Delivery, User.Identity.Name);
                await _changeStatusService.ChangeStatus(Delivery, newState, User.Identity.Name);
				return RedirectToPage("/Index", new { InputDate = Delivery.Date.ToString("dd-MM-yyyy") });
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - ChangeStatusDelivery - {User.Identity.Name} - Error: {ex.Message}");
                return RedirectToPage("/Index");
            }
        }
    }
}
