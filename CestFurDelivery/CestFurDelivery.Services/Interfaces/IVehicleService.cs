using CestFurDelivery.Domain.Entities;

namespace CestFurDelivery.Services.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAll(string username);
        Task<Vehicle> GetById(Guid id, string username);
        Task<bool> CheckInstance(Guid id, string username);
    }
}