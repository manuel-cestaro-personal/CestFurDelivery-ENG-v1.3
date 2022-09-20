using CestFurDelivery.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Domain.Views
{
    public interface IViewDelivery
    {
        Delivery? Delivery { get; set; }
        DeliveryState? DeliveryState { get; set; }
        Team? Team { get; set; }
    }

    public class ViewDelivery : IViewDelivery
    {
        public Delivery? Delivery { get; set; }
        public Team? Team { get; set; }
        public DeliveryState? DeliveryState { get; set; }
    }
}
