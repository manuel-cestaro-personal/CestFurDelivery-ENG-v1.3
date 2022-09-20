using CestFurDelivery.Domain.Views;

namespace CestFurDelivery.Services.Interfaces
{
    public interface IViewService
    {
        Task<List<ViewDay>> GetViewList(DateTime baseDay, string username);
    }
}