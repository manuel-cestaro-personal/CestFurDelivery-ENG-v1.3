using CestFurDelivery.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Services.Services
{
    public class WeekService : IWeekService
    {
        private readonly ILogger<WeekService> _logger;

        public WeekService(ILogger<WeekService> logger)
        {
            _logger = logger;
        }

        public List<DateTime> GetWeek(DateTime day, string username)
        {
            try
            {
                List<DateTime> week = new List<DateTime>();
                switch (day.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                        week.Add(day);
                        week.Add(day.AddDays(1));
                        week.Add(day.AddDays(2));
                        week.Add(day.AddDays(3));
                        week.Add(day.AddDays(4));
                        week.Add(day.AddDays(5));
                        break;
                    case DayOfWeek.Tuesday:
                        week.Add(day.AddDays(-1));
                        week.Add(day);
                        week.Add(day.AddDays(1));
                        week.Add(day.AddDays(2));
                        week.Add(day.AddDays(3));
                        week.Add(day.AddDays(4));
                        break;
                    case DayOfWeek.Wednesday:
                        week.Add(day.AddDays(-2));
                        week.Add(day.AddDays(-1));
                        week.Add(day);
                        week.Add(day.AddDays(1));
                        week.Add(day.AddDays(2));
                        week.Add(day.AddDays(3));
                        break;
                    case DayOfWeek.Thursday:
                        week.Add(day.AddDays(-3));
                        week.Add(day.AddDays(-2));
                        week.Add(day.AddDays(-1));
                        week.Add(day);
                        week.Add(day.AddDays(1));
                        week.Add(day.AddDays(2));
                        break;
                    case DayOfWeek.Friday:
                        week.Add(day.AddDays(-4));
                        week.Add(day.AddDays(-3));
                        week.Add(day.AddDays(-2));
                        week.Add(day.AddDays(-1));
                        week.Add(day);
                        week.Add(day.AddDays(1));
                        break;
                    case DayOfWeek.Saturday:
                        week.Add(day.AddDays(-5));
                        week.Add(day.AddDays(-4));
                        week.Add(day.AddDays(-3));
                        week.Add(day.AddDays(-2));
                        week.Add(day.AddDays(-1));
                        week.Add(day);
                        break;
                    case DayOfWeek.Sunday:
                        week.Add(day.AddDays(-6));
                        week.Add(day.AddDays(-5));
                        week.Add(day.AddDays(-4));
                        week.Add(day.AddDays(-3));
                        week.Add(day.AddDays(-2));
                        week.Add(day.AddDays(-1));
                        break;
                    default:
                        _logger.LogError($"{DateTime.Now} - WeekService - {username} - 404, Day not found");
                        throw new Exception("404, Object not found");
                }
                _logger.LogInformation($"{DateTime.Now} - WeekService - {username} - GetWeek complete successfully");
                return week;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{DateTime.Now} - WeekService - {username} - Error: {ex.Message}");
                throw;
            }
        }
    }
}
