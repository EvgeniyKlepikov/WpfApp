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
    public class ClientManager : BaseManager
    {
        public ClientManager(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        #region basic CRUD operations
        public Client CreateClient(Client client)
        {
            clientRepository.Create(client);
            unitOfWork.SaveChanges();
            return client;
        }
        public void AddRange(List<Client> clients)
        {
            clients.ForEach(c => clientRepository.Create(c));
            unitOfWork.SaveChanges();
        }

        public bool DeleteClient(int id)
        {
            var result = clientRepository.Delete(id);
            if (!result) return false;
            unitOfWork.SaveChanges();
            return true;
        }
        public IEnumerable<Client> FindClient(Expression<Func<Client, bool>> predicate) =>
            clientRepository.Find(predicate);
        public Client GetClientById(int id) => clientRepository.Get(id);
        public IEnumerable<Client> GetAllClients() => clientRepository.GetAll();
        public void UpdateClient(Client client)
        {
            clientRepository.Update(client);
            unitOfWork.SaveChanges();
        }
        #endregion basic CRUD operations
        public void AddPaymentToClient(Payment payment, int clientId)
        {
            var client = clientRepository.Get(clientId);
            payment.ClientId = clientId;
            if (payment.PaymentId <= 0)
                paymentRepository.Create(payment);
            else paymentRepository.Update(payment);
            unitOfWork.SaveChanges();
        }
        public void RemovePaymentFromClient(Payment payment, int clientId)
        {
            var client = clientRepository.Get(clientId, "Payments");
            client.Payments.Remove(payment);
            //application.CargoWeight = 0;
            clientRepository.Update(client);
            paymentRepository.Update(payment);
            unitOfWork.SaveChanges();
        }
        public ICollection<Payment> GetPaymentsOfClient(int clientId) => paymentRepository
            .Find(p => p.ClientId == clientId)
            .ToList();

    }
}
