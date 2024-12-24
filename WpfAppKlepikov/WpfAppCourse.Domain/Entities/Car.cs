using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse.Domain.Entities
{
    public class Car
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public int CarWeight { get; set; }
        public int CarNumber { get; set; }
        // навигационное свойство
        public ICollection<Application> Applications { get; set; }
    }
}
