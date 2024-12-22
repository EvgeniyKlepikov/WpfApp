using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba8.Models
{
    public class DataBaseInitializer : DropCreateDatabaseIfModelChanges<EntityContext>
    {
        protected override void Seed(EntityContext context)
        {
            context.Students.AddRange(new Student[]{
                new Student { FullName="John Smith", Age=23, Payment=234, GroupId=2 },
                new Student { FullName="Uncle Benz", Age=80, Payment=231, GroupId=2 },
                new Student { FullName="Papa Johns", Age=36, Payment=532, GroupId=1 },
            });
        }
    }
}
