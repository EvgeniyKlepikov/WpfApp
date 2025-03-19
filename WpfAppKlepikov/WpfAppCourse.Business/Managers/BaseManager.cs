using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.Business.Managers
{
    public class BaseManager
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IRepository<Application> applicationRepository;
        protected readonly IRepository<Car> carRepository;
        protected readonly IRepository<Client> clientRepository;
        protected readonly IRepository<Driver> driverRepository;
        protected readonly IRepository<Payment> paymentRepository;
        protected readonly IRepository<Route> routeRepository;
        protected readonly IRepository<TechnicalStatus> technicalStatusRepository;

        public BaseManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            applicationRepository = unitOfWork.ApplicationsRepository;
            carRepository = unitOfWork.CarsRepository;
            clientRepository = unitOfWork.ClientsRepository;
            driverRepository = unitOfWork.DriversRepository;
            paymentRepository = unitOfWork.PaymentsRepository;
            routeRepository = unitOfWork.RoutesRepository;
            technicalStatusRepository = unitOfWork.TechnicalStatusesRepository;
        }
    }
}
