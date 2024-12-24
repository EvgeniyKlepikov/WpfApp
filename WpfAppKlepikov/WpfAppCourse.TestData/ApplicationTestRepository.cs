using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WpfAppCourse.Domain.Entities;
using WpfAppCourse.Domain.Interfaces;

namespace WpfAppCourse.TestData
{
    public class ApplicationTestRepository : IRepository<Application>
    {
        private readonly List<Application> applications;

        public ApplicationTestRepository(List<Application> applications)
        {
            this.applications = applications;
        }

        public void Create(Application entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Application> Find(Expression<Func<Application, bool>> predicate)
        {
            Func<Application, bool> filter = predicate.Compile();
            return applications.Where(filter).AsQueryable();
        }

        public Application Get(int id, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Application> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Application entity)
        {
            throw new NotImplementedException();
        }
    }
}
