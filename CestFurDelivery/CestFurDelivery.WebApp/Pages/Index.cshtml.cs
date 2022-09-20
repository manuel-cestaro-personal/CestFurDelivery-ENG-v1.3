using CestFurDelivery.Domain.Views;
using CestFurDelivery.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace CestFurDelivery.WebApp.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IViewService _viewService;

        public IndexModel(ILogger<IndexModel> logger, IViewService viewService)
        {
            _logger = logger;
            _viewService = viewService;
        }

        [BindProperty]
        public DateTime BaseDay { get; set; }
        public List<ViewDay> ViewList { get; set; }

        public async Task<IActionResult> OnGetAsync(string? InputDate)
        {
            try
            {
                if (InputDate != null)
                {
                    DateTime temp = DateTime.Now;
                    CultureInfo provider = new CultureInfo("it-IT");
                    if (DateTime.TryParseExact(InputDate, "dd-MM-yyyy", provider, DateTimeStyles.None, out temp))
                    {
                        BaseDay = temp;
                    }
                    else
                    {
                        BaseDay = DateTime.Now;
                    }
                }
                else
                {
                    BaseDay = DateTime.Now;
                }
                ViewList = await _viewService.GetViewList(BaseDay, User.Identity.Name);
                return Page();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - Index - {User.Identity.Name} - Error: {ex.Message}");
                throw;
            }
        }
        public async Task<IActionResult> OnPostAsync(string submitButton)
        {
            try
            {
                if (submitButton == "Next")
                {
                    BaseDay = BaseDay.AddDays(7);
                }
                else if (submitButton == "Prev")
                {
                    BaseDay = BaseDay.AddDays(-7);
                }
                else
                {
                    _logger.LogWarning($"{DateTime.Now} - Index - {User.Identity.Name} - Button OnPostAsync tampering");
                    return RedirectToPage();
                }
                ViewList = await _viewService.GetViewList(BaseDay, User.Identity.Name);
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