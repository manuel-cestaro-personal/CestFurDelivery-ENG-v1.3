using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CestFurDelivery.Domain.Views
{
    public interface IViewDay
    {
        DateTime Day { get; set; }
        IEnumerable<ViewDelivery>? DeliveriesByDay { get; set; }
        bool TiemCheck { get; set; }
    }

    public class ViewDay : IViewDay
    {
        public DateTime Day { get; set; }
        public IEnumerable<ViewDelivery>? DeliveriesByDay { get; set; }
        public bool TiemCheck { get; set; }
    }
}
