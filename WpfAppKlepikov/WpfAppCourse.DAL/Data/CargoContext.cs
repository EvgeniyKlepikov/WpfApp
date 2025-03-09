using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;

namespace WpfAppCourse.DAL.Data
{
    public class CargoContext : DbContext
    {
        public CargoContext(DbContextOptions<CargoContext> options) : base(options)
        {
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TechnicalStatus> TechnicalStatuses { get; set; }

    }
}
