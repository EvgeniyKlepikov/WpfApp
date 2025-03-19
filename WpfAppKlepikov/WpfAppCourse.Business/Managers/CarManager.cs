using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.Business.Managers
{
    public class CarManager : BaseManager
    {
        public CarManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #region basic CRUD operations
        public IEnumerable<Car> Cars
        {
            get => carRepository.GetAll();
        }
        public Car GetById(int id) => carRepository.Get(id);
        public Car CreateCar(Car car)
        {
            carRepository.Create(car);
            unitOfWork.SaveChanges();
            return car;
        }
        public void AddRange(List<Car> cars)
        {
            cars.ForEach(c => carRepository.Create(c));
            unitOfWork.SaveChanges();
        }
        public bool DeleteCar(int id)
        {
            var result = carRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public void UpdateCar(Car car)
        {
            carRepository.Update(car);
            unitOfWork.SaveChanges();
        }
        public void AddApplicationToCar(Application application, int carId)
        {
            var car = carRepository.Get(carId);
            if (car.CarWeight < application.CargoWeight) return;
            application.CarId = carId;
            if (application.ApplicationId <= 0)
                applicationRepository.Create(application);
            else applicationRepository.Update(application);
            unitOfWork.SaveChanges();
        }
        public void RemoveApplicationFromCar(Application application, int carId)
        {
            var car = carRepository.Get(carId, "Applications");
            car.Applications.Remove(application);
            application.CargoWeight = 0;
            carRepository.Update(car);
            applicationRepository.Update(application);
            unitOfWork.SaveChanges();
        }
        public ICollection<Application> GetApplicationsOfCar(int carId) => applicationRepository
            .Find(a => a.CarId == carId)
            .ToList();

        public void AddDriverToCar(Driver driver, int carId)
        {
            var car = carRepository.Get(carId);
            driver.CarId = carId;
            if (driver.DriverId <= 0)
                driverRepository.Create(driver);
            else driverRepository.Update(driver);
            unitOfWork.SaveChanges();
        }
        public void RemoveDriverFromCar(Driver driver, int carId)
        {
            var car = carRepository.Get(carId, "Drivers");
            car.Drivers.Remove(driver);
            carRepository.Update(car);
            driverRepository.Update(driver);
            unitOfWork.SaveChanges();
        }
        public ICollection<Driver> GetDriversOfCar(int carId) => driverRepository
            .Find(d => d.CarId == carId)
            .ToList();

        public void AddTechnicalStatusToCar(TechnicalStatus technicalStatus, int carId)
        {
            var car = carRepository.Get(carId);
            technicalStatus.CarId = carId;
            if (technicalStatus.TechnicalStatusId <= 0)
                technicalStatusRepository.Create(technicalStatus);
            else technicalStatusRepository.Update(technicalStatus);
            unitOfWork.SaveChanges();
        }
        public void RemoveTechnicalStatusFromCar(TechnicalStatus technicalStatus, int carId)
        {
            var car = carRepository.Get(carId, "TechnicalStatuses");
            car.TechnicalStatuses.Remove(technicalStatus);
            carRepository.Update(car);
            technicalStatusRepository.Update(technicalStatus);
            unitOfWork.SaveChanges();
        }
        public ICollection<TechnicalStatus> GetTechnicalStatusesOfCar(int carId) => technicalStatusRepository
            .Find(t => t.CarId == carId)
            .ToList();

        #endregion basic CRUD operations
    }
}
