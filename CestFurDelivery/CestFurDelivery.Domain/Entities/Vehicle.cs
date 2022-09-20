using CestFurDelivery.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Domain.Entities
{
    public class Vehicle : IVehicle
    {
        public Guid Id { get; set; }
        public string? VehicleName { get; set; }
        public string? Note { get; set; }
    }
}
