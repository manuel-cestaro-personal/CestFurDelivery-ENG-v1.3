
namespace CestFurDelivery.Domain.Interfaces
{
    public interface IVehicle
    {
        Guid Id { get; set; }
        string? Note { get; set; }
        string? VehicleName { get; set; }
    }
}