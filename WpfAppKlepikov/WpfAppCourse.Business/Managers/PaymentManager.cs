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
    public class PaymentManager : BaseManager
    {
        public PaymentManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region basic CRUD operations
        public bool DeletePayment(int id)
        {
            var result = paymentRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<Payment> FindPayment(Expression<Func<Payment, bool>> predicate) =>
            paymentRepository.Find(predicate);
        public Payment GetPaymentById(int id) => paymentRepository.Get(id);
        public IEnumerable<Payment> GetAllPayments() => paymentRepository.GetAll();
        public void UpdatePayment(Payment payment)
        {
            paymentRepository.Update(payment);
            unitOfWork.SaveChanges();
        }
        #endregion basic CRUD operations

    }
}
