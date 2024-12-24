using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.TestData
{
    public class CarTestRepository : IRepository<Car>
    {
        private readonly List<Car> cars;

        public CarTestRepository(List<Car> cars)
        {
            this.cars = cars;
            SetupData();
        }

        private void SetupData()
        {
            var s = 1;
            Random rnd = new Random();
            for (var i = 1; i <= 5; i++)
            {
                var car = new Car
                {
                    CarWeight = rnd.Next(1, 50),
                    CarNumber = rnd.Next(1000, 9999),
                    CarName = $"Автомобиль {i}",
                    CarId = i
                };
                var applications = new List<Application>();
                for (var j = 0; j < 10; j++)
                {
                    applications.Add(new Application
                    {
                        CarId = i,
                        DateOfDispatch = DateTime.Now -
                    TimeSpan.FromDays(rnd.Next(6000, 20000)),
                        CargoName = $"Груз {s}",
                        ApplicationId = s,
                        CargoWeight = rnd.Next(1, 50)
                    });
                    s++;
                }
                car.Applications = applications;
                cars.Add(car);
            }
        }
        public void Create(Car entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Car> Find(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Car Get(int id, params string[] includes)
        {
            return cars.FirstOrDefault(g => g.CarId == id);
        }

        public IQueryable<Car> GetAll()
        {
            return cars.AsQueryable();
        }

        public void Update(Car entity)
        {
            throw new NotImplementedException();
        }
    }
}
