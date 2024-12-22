using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba8.Models
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("DefaultConnection")
        {
        }
        public DbSet<Student> Students { get; set; }    
    }
}
