using CestFurDelivery.Domain.Entities;

namespace CestFurDelivery.Services.Interfaces
{
    public interface IChangeStatusService
    {
        Task ChangeStatus(Delivery model, Guid StausID, string username);
        Task<Guid> FindStatus(Delivery delivery, string username);
    }
}