using CestFurDelivery.Domain.Entities;

namespace CestFurDelivery.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAll(string username);
        Task<Team> GetById(Guid id, string username);
        Task<bool> CheckInstance(Guid id, string username);
    }
}