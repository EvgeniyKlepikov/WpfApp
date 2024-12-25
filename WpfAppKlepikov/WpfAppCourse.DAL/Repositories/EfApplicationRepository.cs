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
    public class EfApplicationRepository : IRepository<Application>
    {
        private readonly CargoContext context;
        private readonly DbSet<Application> applications;
        public EfApplicationRepository(CargoContext context)
        {
            this.context = context;
            applications = context.Applications;
        }
        public void Create(Application entity)
        {
            applications.AddAsync(entity);
        }

        public bool Delete(int id)
        {
            var application = applications.Find(id);
            if (application == null) return false;
            if (application.CarId > 0)
            {
                context.Cars
                    .Find(application.CarId)
                    .Applications
                    .Remove(application);
            };
            applications.Remove(application);
            return true;
        }

        public IQueryable<Application> Find(Expression<Func<Application, bool>> predicate)
        {
            return applications.Where(predicate);
        }

        public Application Get(int id, params string[] includes)
        {
            IQueryable<Application> query = applications;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(a => a.ApplicationId == id);
        }

        public IQueryable<Application> GetAll()
        {
            return applications.AsQueryable();
        }

        public void Update(Application entity)
        {
            applications.Update(entity);
        }
    }
}
