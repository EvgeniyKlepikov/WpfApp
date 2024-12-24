using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;

namespace WpfAppCourse.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Application> ApplicationsRepository { get; }
        IRepository<Car> CarsRepository { get; }
        void SaveChanges();
    }
}
