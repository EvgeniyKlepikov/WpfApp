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
        public int Total { get; set; }
        public bool Status { get; set; }
        // Навигационные свойства
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }
}
