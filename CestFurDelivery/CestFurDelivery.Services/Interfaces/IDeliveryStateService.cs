using CestFurDelivery.Domain.Entities;

namespace CestFurDelivery.Services.Interfaces
{
    public interface IDeliveryStateService
    {
        Task<IEnumerable<DeliveryState>> GetAll(string username);
        Task<DeliveryState> GetById(Guid id, string username);
        Task<bool> CheckInstance(Guid id, string username);
    }
}