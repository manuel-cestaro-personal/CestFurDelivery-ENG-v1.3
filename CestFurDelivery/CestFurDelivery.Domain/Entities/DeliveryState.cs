using CestFurDelivery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Domain.Entities
{
    public class DeliveryState : IDeliveryState
    {
        public Guid Id { get; set; }
        public string? State { get; set; }
        public string? Icon { get; set; }
    }
}
