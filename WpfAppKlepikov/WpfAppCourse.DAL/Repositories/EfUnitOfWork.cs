using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.DAL.Data;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.DAL.Repositories
{
    public class EfUnitOfWork:IUnitOfWork
    {
        private readonly CargoContext context;
        private IRepository<Application> applicationsRepository;
        private IRepository<Car> carsRepository;
        private IRepository<Client> clientsRepository;
        private IRepository<Driver> driversRepository;
        private IRepository<Payment> paymentsRepository;
        private IRepository<Route> routesRepository;
        private IRepository<TechnicalStatus> technicalStatusesRepository;

        public EfUnitOfWork(string connectionString)
        {
            var options = new DbContextOptionsBuilder<CargoContext>()
            .UseSqlServer(connectionString)
            .Options;
            context = new CargoContext(options);
            context.Database.EnsureCreated();
        }
        public IRepository<Application> ApplicationsRepository 
            => applicationsRepository ?? new EfApplicationRepository(context);
        public IRepository<Car> CarsRepository 
            => carsRepository ?? new EfCarsRepository(context);
        public IRepository<Client> ClientsRepository
            => clientsRepository ?? new EfClientsRepository(context);
        public IRepository<Driver> DriversRepository
            => driversRepository ?? new EfDriversRepository(context);
        public IRepository<Payment> PaymentsRepository
            => paymentsRepository ?? new EfPaymentsRepository(context);
        public IRepository<Route> RoutesRepository
            => routesRepository ?? new EfRoutesRepository(context);
        public IRepository<TechnicalStatus> TechnicalStatusesRepository
            => technicalStatusesRepository ?? new EfTechnicalStatusesRepository(context);

        public void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
