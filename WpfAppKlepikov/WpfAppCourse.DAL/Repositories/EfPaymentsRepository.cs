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
    public class EfPaymentsRepository : IRepository<Payment>
    {
        private readonly CargoContext context;
        private readonly DbSet<Payment> payments;

        public EfPaymentsRepository(CargoContext context)
        {
            this.context = context;
            payments = context.Payments;
        }

        public void Create(Payment entity)
        {
            payments.AddAsync(entity);
        }

        public bool Delete(int id)
        {
            var payment = payments.Find(id);
            if (payment == null) return false;
            if (payment.ApplicationId > 0)
            {
                context.Applications
                    .Find(payment.ApplicationId)
                    .Payments
                    .Remove(payment);
            };
            payments.Remove(payment);
            return true;
        }

        public IQueryable<Payment> Find(Expression<Func<Payment, bool>> predicate)
        {
            return payments.Where(predicate);
        }

        public Payment Get(int id, params string[] includes)
        {
            IQueryable<Payment> query = payments;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(p => p.PaymentId == id);
        }

        public IQueryable<Payment> GetAll()
        {
            return payments.AsQueryable();
        }

        public void Update(Payment entity)
        {
            payments.Update(entity);
        }

    }
}
