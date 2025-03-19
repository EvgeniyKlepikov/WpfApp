using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.Business.Managers
{
    public class RouteManager : BaseManager
    {
        public RouteManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region basic CRUD operations
        public bool DeleteRoute(int id)
        {
            var result = routeRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<Route> FindRoute(Expression<Func<Route, bool>> predicate) =>
            routeRepository.Find(predicate);
        public Route GetRouteById(int id) => routeRepository.Get(id);
        public IEnumerable<Route> GetAllRoutes() => routeRepository.GetAll();
        public void UpdateRoute(Route route)
        {
            routeRepository.Update(route);
            unitOfWork.SaveChanges();
        }
        #endregion basic CRUD operations

    }
}
