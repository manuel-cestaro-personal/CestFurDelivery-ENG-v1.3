using CestFurDelivery.Domain.Views;
using CestFurDelivery.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Services.Services
{
    public class ViewService : IViewService
    {
        private readonly ILogger<ViewService> _logger;
        private readonly IDeliveryService _deliveryService;
        private readonly IDeliveryStateService _deliveryStateService;
        private readonly ITeamService _teamService;
        private readonly IWeekService _weekService;

        public ViewService(ILogger<ViewService> logger,
            IDeliveryService deliveryService,
            IDeliveryStateService deliveryStateService,
            ITeamService teamService,
            IWeekService weekService)
        {
            _logger = logger;
            _deliveryService = deliveryService;
            _deliveryStateService = deliveryStateService;
            _teamService = teamService;
            _weekService = weekService;
        }
        public async Task<List<ViewDay>> GetViewList(DateTime baseDay, string username)
        {
            try
            {
                List<ViewDay> viewList = new List<ViewDay>();
                List<DateTime> days = _weekService.GetWeek(baseDay, username);
                foreach (var day in days)
                {
                    ViewDay viewDay = new ViewDay();

                    viewDay.Day = day;
                    List<ViewDelivery> viewDeliveries = new List<ViewDelivery>();
                    viewDay.TiemCheck = false;
                    var deliveries = await _deliveryService.GetByDate(day, username);
                    if (deliveries != null)
                    {
                        foreach (var delivery in deliveries)
                        {
                            ViewDelivery viewDelivery = new ViewDelivery();
                            viewDelivery.Delivery = delivery;
                            var team = await _teamService.GetById(delivery.Team, username);
                            if (team != null)
                            {
                                viewDelivery.Team = team;
                            }
                            var state = await _deliveryStateService.GetById(delivery.IdDeliveryState, username);
                            if (state != null)
                            {
                                viewDelivery.DeliveryState = state;
                            }
                            viewDeliveries.Add(viewDelivery);
                        }
                        
                    }
                    viewDay.DeliveriesByDay = viewDeliveries;
                    foreach (var delivery in viewDay.DeliveriesByDay)
                    {
                        if (!viewDay.TiemCheck)
                        {
                            foreach (var subDelivery in viewDay.DeliveriesByDay)
                            {
                                if (delivery != subDelivery)
                                {
                                    if (TimeSpan.Compare(delivery.Delivery.TimeStart, subDelivery.Delivery.TimeStart) == 0 ||
                                        TimeSpan.Compare(delivery.Delivery.TimeEnd, subDelivery.Delivery.TimeEnd) == 0 ||
                                        TimeSpan.Compare(delivery.Delivery.TimeStart, subDelivery.Delivery.TimeEnd) == 0 ||
                                        TimeSpan.Compare(delivery.Delivery.TimeEnd, subDelivery.Delivery.TimeStart) == 0 ||
                                        (TimeSpan.Compare(delivery.Delivery.TimeStart, subDelivery.Delivery.TimeStart) == 1 && TimeSpan.Compare(delivery.Delivery.TimeStart, subDelivery.Delivery.TimeEnd) == -1) ||
                                        (TimeSpan.Compare(delivery.Delivery.TimeEnd, subDelivery.Delivery.TimeStart) == 1 && TimeSpan.Compare(delivery.Delivery.TimeEnd, subDelivery.Delivery.TimeEnd) == -1))
                                    {
                                        viewDay.TiemCheck = true;
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    viewList.Add(viewDay);
                }
                _logger.LogInformation($"{DateTime.Now} - ViewService - {username} - Building ViewDay list complete successfully");
                return viewList;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - ViewService - {username} - Error: {ex.Message}");
                throw;
            }
        }
    }
}
