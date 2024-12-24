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
    public class ApplicationManager : BaseManager
    {
        public ApplicationManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #region basic CRUD operations
        public bool DeleteApplication(int id)
        {
            var result = applicationRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<Application> FindApplication(Expression<Func<Application, bool>> predicate) =>
            applicationRepository.Find(predicate);
        public Application GetApplicationById(int id) => applicationRepository.Get(id);
        public IEnumerable<Application> GetAllApplications() => applicationRepository.GetAll();
        public void UpdateApplication(Application application)
        {
            applicationRepository.Update(application);
            unitOfWork.SaveChanges();
        }

        #endregion basic CRUD operations
    }
}
