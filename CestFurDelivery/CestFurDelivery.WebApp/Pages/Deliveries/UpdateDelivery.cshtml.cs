using CestFurDelivery.Domain.Entities;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CestFurDelivery.WebApp.Pages.Deliveries
{
    [Authorize(Roles = "Admin,ReadWrite")]
    public class UpdateDeliveryModel : PageModel
    {
        private readonly ILogger<UpdateDeliveryModel> _logger;
        private readonly IDeliveryService _deliveryService;
        private readonly IDeliveryStateService _deliveryStateService;
        private readonly ITeamService _teamService;
        private readonly IVehicleService _vehicleService;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UpdateDeliveryModel(ILogger<UpdateDeliveryModel> logger,
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

        [BindProperty]
        public Delivery Delivery { get; set; }
        public IEnumerable<Team> TeamList { get; set; }
        public IEnumerable<Vehicle> VehicleList { get; set; }
        public IEnumerable<DeliveryState> DeliveryStateList { get; set; }
        public bool CheckTimes { get; set; }
        public bool CheckDate { get; set; }

        public async Task<IActionResult> OnGetAsync(string Id)
        {
            try
            {
                CheckTimes = false;
				CheckDate = false;
                DeliveryStateList = await _deliveryStateService.GetAll(User.Identity.Name);
                VehicleList = await _vehicleService.GetAll(User.Identity.Name);
                TeamList = await _teamService.GetAll(User.Identity.Name);
                Delivery = await _deliveryService.GetById(Guid.Parse(Id), User.Identity.Name);
                if (Delivery == null)
                    return NotFound();
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
                CheckTimes = false;
				CheckDate = false;
                Delivery.Id = Guid.Parse(Id);
                if (ModelState.IsValid)
                {
                    if (Delivery.Date.DayOfWeek != DayOfWeek.Sunday)
                    {
                        if (TimeSpan.Compare(Delivery.TimeStart, Delivery.TimeEnd) == -1)
                        {
                            bool check1 = await _deliveryStateService.CheckInstance(Delivery.IdDeliveryState, User.Identity.Name);
                            bool check2 = await _vehicleService.CheckInstance(Delivery.Vehicle1, User.Identity.Name);
                            bool check3 = true;
                            bool check4 = true;
                            if (Delivery.Vehicle2 != null)
                            {
                                check3 = await _vehicleService.CheckInstance((Guid)Delivery.Vehicle2, User.Identity.Name);
                            }
                            if (Delivery.Vehicle3 != null)
                            {
                                check4 = await _vehicleService.CheckInstance((Guid)Delivery.Vehicle3, User.Identity.Name);
                            }
                            bool check5 = await _teamService.CheckInstance(Delivery.Team, User.Identity.Name);
                            if (check1 && check2 && check3 && check4 && check5)
                            {
                                await _deliveryService.Update(Delivery, User.Identity.Name);
                                return RedirectToPage("/Index", new { InputDate = Delivery.Date.ToString("dd-MM-yyyy") });
                            }
                            return RedirectToPage();
                        }
                        else
                        {
                            CheckTimes = true;
                        }
                    }
                    else
                    {
                        CheckDate = false;
                    }
                }

                DeliveryStateList = await _deliveryStateService.GetAll(User.Identity.Name);
                VehicleList = await _vehicleService.GetAll(User.Identity.Name);
                TeamList = await _teamService.GetAll(User.Identity.Name);
                Delivery = await _deliveryService.GetById((Guid)Delivery.Id, User.Identity.Name);
                if (Delivery == null)
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
