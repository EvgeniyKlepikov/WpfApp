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
    public class DriverManager : BaseManager
    {
        public DriverManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #region basic CRUD operations
        public bool DeleteDriver(int id)
        {
            var result = driverRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<Driver> FindDriver(Expression<Func<Driver, bool>> predicate) =>
            driverRepository.Find(predicate);
        public Driver GetDriverById(int id) => driverRepository.Get(id);
        public IEnumerable<Driver> GetAllDrivers() => driverRepository.GetAll();
        public void UpdateDriver(Driver driver)
        {
            driverRepository.Update(driver);
            unitOfWork.SaveChanges();
        }

        #endregion basic CRUD operations

    }
}
