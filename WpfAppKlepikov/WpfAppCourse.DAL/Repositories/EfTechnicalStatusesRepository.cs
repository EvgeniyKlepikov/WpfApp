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
    public class EfTechnicalStatusesRepository : IRepository<TechnicalStatus>
    {
        private readonly CargoContext context;
        private readonly DbSet<TechnicalStatus> technicalStatuses;

        public EfTechnicalStatusesRepository(CargoContext context)
        {
            this.context = context;
            technicalStatuses = context.TechnicalStatuses;
        }

        public void Create(TechnicalStatus entity)
        {
            technicalStatuses.AddAsync(entity);
        }

        public bool Delete(int id)
        {
            var technicalStatuse = technicalStatuses.Find(id);
            if (technicalStatuse == null) return false;
            if (technicalStatuse.CarId > 0)
            {
                context.Cars
                    .Find(technicalStatuse.CarId)
                    .TechnicalStatuses
                    .Remove(technicalStatuse);
            };
            technicalStatuses.Remove(technicalStatuse);
            return true;
        }

        public IQueryable<TechnicalStatus> Find(Expression<Func<TechnicalStatus, bool>> predicate)
        {
            return technicalStatuses.Where(predicate);
        }

        public TechnicalStatus Get(int id, params string[] includes)
        {
            IQueryable<TechnicalStatus> query = technicalStatuses;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(t => t.TechnicalStatusId == id);
        }

        public IQueryable<TechnicalStatus> GetAll()
        {
            return technicalStatuses.AsQueryable();
        }

        public void Update(TechnicalStatus entity)
        {
            technicalStatuses.Update(entity);
        }

    }
}
