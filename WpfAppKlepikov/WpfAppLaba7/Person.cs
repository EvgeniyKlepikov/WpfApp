using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba7
{
    public class Person
    {
        static string connectionString;
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateBirth { get; set; }
        public Company Job { get; set; }
        public decimal Salary { get; set; }
        static Person()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
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

        #region CRUD
        public static Person GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Person WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var company = Company.GetById((int)reader["CompanyId"]);
                    return new Person(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (DateTime)reader["DateOfBirth"],
                        company,
                        (decimal)reader["Salary"]);
                }
                return null;
            }
        }
        public static ObservableCollection<Person> GetByCompanyId(int CompanyId)
        {
            ObservableCollection<Person> collection = new ObservableCollection<Person>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Person WHERE CompanyId = @CompanyId";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CompanyId", CompanyId);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var company = Company.GetById((int)reader["CompanyId"]);
                    collection.Add(
                        new Person(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (DateTime)reader["DateOfBirth"],
                        company,
                        (decimal)reader["Salary"])
                        );
                }
                return collection;
            }
        }

        public static ObservableCollection<Person> GetAll()
        {
            ObservableCollection<Person> collection = new ObservableCollection<Person>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Person";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var company = Company.GetById((int)reader["CompanyId"]);
                    collection.Add(
                        new Person(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        (DateTime)reader["DateOfBirth"],
                        company,
                        (decimal)reader["Salary"])
                        );
                }
                return collection;
            }
        }

        public void Update()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "UPDATE Person SET Name = @Name, DateOfBirth = @DateOfBirth, CompanyId = @CompanyId, Salary = @Salary WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@DateOfBirth", DateBirth);
                command.Parameters.AddWithValue("@CompanyId", Job.Id);
                command.Parameters.AddWithValue("@Salary", Salary);
                command.Parameters.AddWithValue("@Id", ID);
                command.ExecuteNonQuery();
            }
        }

        public void Insert()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Person (Name, DateOfBirth, CompanyId, Salary) OUTPUT INSERTED.Id VALUES (@Name, @DateOfBirth, @CompanyId, @Salary)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@DateOfBirth", DateBirth);
                command.Parameters.AddWithValue("@CompanyId", Job.Id);
                command.Parameters.AddWithValue("@Salary", Salary);
                ID = (int)command.ExecuteScalar();
            }
        }

        public void Delete()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Person WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion CRUD
        public override string? ToString()
        {
            return $"ID: {ID}, Name: {Name}, Date of birth: {DateBirth:D}, Company: {Job?.Name}, Salary: {Salary:C}";
        }
    }
}
