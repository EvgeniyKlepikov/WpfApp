using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppCourse.Business.Managers;
using WpfAppCourse.Domain.Interfaces;
using WpfAppCourse.TestData;

namespace WpfAppCourse.Business.Infrastructure
{
    public class ManagersFactory
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationManager applicationManager;
        private readonly CarManager carManager;

        public ManagersFactory()
        {
            unitOfWork = new TestUnitOfWork();
        }
        public ApplicationManager GetApplicationManager()
        {
            return applicationManager
            ?? new ApplicationManager(unitOfWork);
        }
        public CarManager GetCarManager()
        {
            return carManager
            ?? new CarManager(unitOfWork);
        }
    }
}
