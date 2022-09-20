
namespace CestFurDelivery.Domain.Interfaces
{
    public interface IDelivery
    {
        string? City { get; set; }
        string? ClientName { get; set; }
        string? ClientSurname { get; set; }
        DateTime Date { get; set; }
        string? Furniture { get; set; }
        Guid? Id { get; set; }
        Guid IdDeliveryState { get; set; }
        string? Note { get; set; }
        Guid Team { get; set; }
        TimeSpan TimeEnd { get; set; }
        TimeSpan TimeStart { get; set; }
        Guid Vehicle1 { get; set; }
        Guid? Vehicle2 { get; set; }
        Guid? Vehicle3 { get; set; }
    }
}