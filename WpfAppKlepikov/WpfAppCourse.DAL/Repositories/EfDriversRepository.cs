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
    public class EfDriversRepository : IRepository<Driver>
    {
        private readonly CargoContext context;
        private readonly DbSet<Driver> drivers;

        public EfDriversRepository(CargoContext context)
        {
            this.context = context;
            drivers = context.Drivers;
        }

        public void Create(Driver entity)
        {
            drivers.AddAsync(entity);
        }

        public bool Delete(int id)
        {
            var driver = drivers.Find(id);
            if (driver == null) return false;
            if (driver.CarId > 0)
            {
                context.Cars
                    .Find(driver.CarId)
                    .Drivers
                    .Remove(driver);
            };
            drivers.Remove(driver);
            return true;
        }

        public IQueryable<Driver> Find(Expression<Func<Driver, bool>> predicate)
        {
            return drivers.Where(predicate);
        }

        public Driver Get(int id, params string[] includes)
        {
            IQueryable<Driver> query = drivers;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(d => d.DriverId == id);
        }

        public IQueryable<Driver> GetAll()
        {
            return drivers.AsQueryable();
        }

        public void Update(Driver entity)
        {
            drivers.Update(entity);
        }


    }
}
