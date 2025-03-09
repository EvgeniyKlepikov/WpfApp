using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse.Domain.Entities
{
    public class TechnicalStatus
    {
        public int TechnicalStatusId { get; set; }
        public DateTime DateOfStatus { get; set; }
        // Навигационные свойства
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
