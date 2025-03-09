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
        protected readonly IRepository<Driver> driverRepository;


        public BaseManager(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            applicationRepository = unitOfWork.ApplicationsRepository;
            carRepository = unitOfWork.CarsRepository;
            driverRepository = unitOfWork.DriversRepository;
        }
    }
}
