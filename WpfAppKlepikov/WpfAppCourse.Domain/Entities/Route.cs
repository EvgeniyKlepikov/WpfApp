using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse.Domain.Entities
{
    public class Route
    {
        public int RouteId { get; set; }
        public string Destination { get; set; }
        public int Distance { get; set; }
        // Навигационные свойства
        public int CarId { get; set; }
        public Car Car { get; set; }

        //public ICollection<Car> Cars { get; set; }
        //public ICollection<Application> Applications { get; set; }

    }
}
