using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba7
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Headquarters { get; set; }
        public DateTime DateEstablishment { get; set; }
        public Company() { }
        public Company(int id, string name, string? headquarters, DateTime dateEstablishment)
        {
            Id = id;
            Name = name;
            Headquarters = headquarters;
            DateEstablishment = dateEstablishment;
        }

        public override string? ToString()
        {
            return $"ID: {Id}, Name: {Name}, Headquarters: {Headquarters}, DateEstablishment: {DateEstablishment:D}";
        }
    }
}
