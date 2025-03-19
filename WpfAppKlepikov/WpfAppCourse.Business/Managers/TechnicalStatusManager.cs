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
    public class TechnicalStatusManager : BaseManager
    {
        public TechnicalStatusManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region basic CRUD operations
        public bool DeleteTechnicalStatus(int id)
        {
            var result = technicalStatusRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<TechnicalStatus> FindTechnicalStatus(Expression<Func<TechnicalStatus, bool>> predicate) =>
            technicalStatusRepository.Find(predicate);
        public TechnicalStatus GetTechnicalStatusById(int id) => technicalStatusRepository.Get(id);
        public IEnumerable<TechnicalStatus> GetAllTechnicalStatuses() => technicalStatusRepository.GetAll();
        public void UpdateTechnicalStatus(TechnicalStatus technicalStatus)
        {
            technicalStatusRepository.Update(technicalStatus);
            unitOfWork.SaveChanges();
        }
        #endregion basic CRUD operations

    }
}
