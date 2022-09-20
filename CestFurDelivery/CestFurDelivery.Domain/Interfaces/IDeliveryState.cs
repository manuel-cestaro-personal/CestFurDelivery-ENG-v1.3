
namespace CestFurDelivery.Domain.Interfaces
{
    public interface IDeliveryState
    {
        Guid Id { get; set; }
        string? State { get; set; }
        string? Icon { get; set; }
    }
}