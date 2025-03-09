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
using static System.Net.Mime.MediaTypeNames;

namespace WpfAppCourse.DAL.Repositories
{
    public class EfClientsRepository : IRepository<Client>
    {
        private readonly CargoContext context;
        private readonly DbSet<Client> clients;

        public EfClientsRepository(CargoContext context)
        {
            this.context = context;
            clients = context.Clients;
        }

        public void Create(Client entity)
        {
            clients.AddAsync(entity);
        }

        public bool Delete(int id)
        {
            var client = clients.Find(id);
            if (client == null) return false;
            if (client.ApplicationId > 0)
            {
                context.Applications
                    .Find(client.ApplicationId)
                    .Clients
                    .Remove(client);
            };
            clients.Remove(client);
            return true;
        }

        public IQueryable<Client> Find(Expression<Func<Client, bool>> predicate)
        {
            return clients.Where(predicate);
        }

        public Client Get(int id, params string[] includes)
        {
            IQueryable<Client> query = clients;
            foreach (var include in includes)
                query = query.Include(include);
            return query.First(c => c.ClientId == id);
        }

        public IQueryable<Client> GetAll()
        {
            return clients.AsQueryable();
        }

        public void Update(Client entity)
        {
            clients.Update(entity);
        }

    }
}
