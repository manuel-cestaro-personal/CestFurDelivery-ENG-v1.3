
namespace CestFurDelivery.Services.Interfaces
{
    public interface IWeekService
    {
        List<DateTime> GetWeek(DateTime day, string username);
    }
}