using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse.Domain.Entities
{
    public class Driver
    {
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public string DriverSurname { get; set; }
        public int DriverAge { get; set; }
        public int DriverExperience { get; set; }
        public DateTime DateOfAdmission { get; set; }
        // Навигационные свойства
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}
