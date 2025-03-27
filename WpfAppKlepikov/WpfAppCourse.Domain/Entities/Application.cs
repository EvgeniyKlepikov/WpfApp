using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WpfAppCourse.Domain.Entities
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public string CargoName { get; set; }
        public DateTime DateOfDispatch { get; set; }
        public int CargoWeight { get; set; }
        public string Destination { get; set; }
        // Навигационные свойства
        public int CarId { get; set; }
        public Car Car { get; set; }
        //public int ClientId { get; set; }
        //public Client Client { get; set; }
        //public int RouteId { get; set; }
        //public Route Route { get; set; }
        //public ICollection<Payment> Payments { get; set; }

    }
}
