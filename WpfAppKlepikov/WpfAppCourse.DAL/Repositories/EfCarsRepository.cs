using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfAppCourse.DAL.Data;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.DAL.Repositories
{
    public class EfCarsRepository : IRepository<Car>
    {
        private readonly DbSet<Car> cars;

        public EfCarsRepository(CargoContext cargoContext)
        {
            this.cars = cargoContext.Cars;
        }

        public void Create(Car entity)
        {
            cars.Add(entity);
        }

        public bool Delete(int id)
        {
            var car = cars.Find(id);
            if (car == null) return false;
            cars.Remove(car);
            return true;
        }

        public IQueryable<Car> Find(Expression<Func<Car, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Car Get(int id, params string[] includes)
        {
            IQueryable<Car> query = cars;
            foreach (var include in includes)
            query = query.Include(include);
            return query.First(c => c.CarId == id);
        }

        public IQueryable<Car> GetAll()
        {
            return cars.AsQueryable();
        }

        public void Update(Car entity)
        {
            cars.Update(entity);
        }
    }
}
