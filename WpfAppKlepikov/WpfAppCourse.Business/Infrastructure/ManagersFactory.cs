using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCourse.Business.Managers;
using WpfAppCourse.DAL.Repositories;
using WpfAppCourse.Domain.Interfaces;
using WpfAppCourse.TestData;

namespace WpfAppCourse.Business.Infrastructure
{
    public class ManagersFactory
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationManager applicationManager;
        private readonly CarManager carManager;
        private readonly ClientManager clientManager;
        private readonly DriverManager driverManager;
        private readonly PaymentManager paymentManager;
        private readonly RouteManager routeManager;
        private readonly TechnicalStatusManager technicalStatusManager;

        public ManagersFactory(string connStringName)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connString = configuration
            .GetConnectionString(connStringName);
            unitOfWork = new EfUnitOfWork(connString);
        }
        public ManagersFactory()
        {
            unitOfWork = new TestUnitOfWork();
        }
        public ApplicationManager GetApplicationManager()
        {
            return applicationManager
            ?? new ApplicationManager(unitOfWork);
        }
        public CarManager GetCarManager()
        {
            return carManager
            ?? new CarManager(unitOfWork);
        }
        public ClientManager GetClientManager()
        {
            return clientManager
            ?? new ClientManager(unitOfWork);
        }
        public DriverManager GetDriverManager()
        {
            return driverManager
            ?? new DriverManager(unitOfWork);
        }
        public PaymentManager GetPaymentManager()
        {
            return paymentManager
            ?? new PaymentManager(unitOfWork);
        }
        public RouteManager GetRouteManager()
        {
            return routeManager
            ?? new RouteManager(unitOfWork);
        }
        public TechnicalStatusManager GetTechnicalStatusManager()
        {
            return technicalStatusManager
            ?? new TechnicalStatusManager(unitOfWork);
        }
    }
}
