using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppCourse.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public double TotalInsurance { get; set; }
        public double TotalTrip { get; set; }
        public double TotalSum { get; set; }
        public DateTime DateOfRegistration { get; set; }
        //public bool Status { get; set; }
        // Навигационные свойства
        public int ClientId { get; set; }
        public Client client { get; set; }
    }
}
