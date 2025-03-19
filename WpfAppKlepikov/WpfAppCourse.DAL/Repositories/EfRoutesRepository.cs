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
    public class EfRoutesRepository : IRepository<Route>
    {
        private readonly DbSet<Route> routes;

        public EfRoutesRepository(CargoContext context)
        {
            routes = context.Routes;
        }

        public void Create(Route entity)
        {
            routes.AddAsync(entity);
        }

        public bool Delete(int id)
        {
            var route = routes.Find(id);
            if (route == null) return false;
            routes.Remove(route);
            return true;
        }

        public IQueryable<Route> Find(Expression<Func<Route, bool>> predicate)
        {
            return routes.Where(predicate);
        }

        public Route Get(int id, params string[] includes)
        {
            IQueryable<Route> query = routes;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(r => r.RouteId == id);
        }

        public IQueryable<Route> GetAll()
        {
            return routes.AsQueryable();
        }

        public void Update(Route entity)
        {
            routes.Update(entity);
        }

    }
}
