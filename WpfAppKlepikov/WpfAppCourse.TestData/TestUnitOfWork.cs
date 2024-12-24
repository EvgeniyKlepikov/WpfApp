using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.TestData
{
    public class TestUnitOfWork : IUnitOfWork
    {
        private IRepository<Application> applicationsRepository;
        private IRepository<Car> carsRepository;
        private List<Car> cars;
        private List<Application> applications;

        public TestUnitOfWork()
        {
            cars = new List<Car>();
            applications = new List<Application>();
            carsRepository = new CarTestRepository(cars);
            foreach (var car in cars)
                applications.AddRange(car.Applications);
            applicationsRepository = new ApplicationTestRepository(applications);
        }

        public IRepository<Application> ApplicationsRepository => applicationsRepository;

        public IRepository<Car> CarsRepository => carsRepository;

        public void SaveChanges()
        {
        }
    }
}
