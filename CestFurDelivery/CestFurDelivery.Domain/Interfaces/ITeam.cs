
namespace CestFurDelivery.Domain.Interfaces
{
    public interface ITeam
    {
        string? Description { get; set; }
        Guid Id { get; set; }
        string? Name { get; set; }
    }
}