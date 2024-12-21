using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba7
{
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public Company Job { get; set; }
        public decimal Salary { get; set; }
        public Person()
        {
            DateBirth = DateTime.MinValue;
        }
        public Person(int id, string name, DateTime dateBirth, Company job, decimal salary)
        {
            ID = id;
            Name = name;
            DateBirth = dateBirth;
            Job = job;
            Salary = salary;
        }
        public override string? ToString()
        {
            return $"ID: {ID}, Name: {Name}, Date of birth: {DateBirth:D}, Company: {Job?.Name}, Salary: {Salary:C}";
        }
    }
}
