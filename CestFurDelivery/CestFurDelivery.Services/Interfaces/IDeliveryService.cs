using CestFurDelivery.Domain.Entities;

namespace CestFurDelivery.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task Delete(Guid id, string username);
        Task<IEnumerable<Delivery>> GetByDate(DateTime date, string username);
        Task<Delivery> GetById(Guid id, string username);
        Task Insert(Delivery model, string username);
        Task Update(Delivery model, string username);
        Task<bool> CheckInstance(Guid id, string username);
    }
}