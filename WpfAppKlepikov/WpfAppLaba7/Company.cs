using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba7
{
    public class Company
    {
        static string connectionString;
        static Company()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
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
        #region CRUD
        public static Company GetById(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Company WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                if (reader.Read()) 
                {
                    return new Company(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        reader["Headquarters"] == DBNull.Value ? null : (string)reader["Headquarters"],
                        (DateTime)reader["DateOfEstablishment"]);
                }
                return null;
            }
        }
        public static ObservableCollection<Company> GetAll()
        {
            ObservableCollection<Company> collection = new ObservableCollection<Company>();
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM Company";
                var command = new SqlCommand(query, connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    collection.Add(
                        new Company(
                        (int)reader["Id"],
                        (string)reader["Name"],
                        reader["Headquarters"] == DBNull.Value ? null : (string)reader["Headquarters"],
                        (DateTime)reader["DateOfEstablishment"])
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
                var query = "UPDATE Company SET Name = @Name, Headquarters = @Headquarters, DateOfEstablishment = @DateOfEstablishment WHERE Id = @Id";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Headquarters", Headquarters ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateOfEstablishment", DateEstablishment);
                command.Parameters.AddWithValue("@Id", Id);
                command.ExecuteNonQuery();
            }
        }

        public void Insert()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Company (Name, Headquarters, DateOfEstablishment) OUTPUT INSERTED.Id VALUES (@Name, @Headquarters, @DateOfEstablishment)";
                var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", Name);
                command.Parameters.AddWithValue("@Headquarters", Headquarters ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateOfEstablishment", DateEstablishment);
                Id = (int)command.ExecuteScalar();
            }
        }

        public void Delete()
        {
            //Сначала удаляем всех сотрудников этой компании
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Person WHERE CompanyId = @CompanyId", connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", Id);
                    command.ExecuteNonQuery();
                }
            }

            //Затем удаляем саму компанию
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Company WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.ExecuteNonQuery();
                }
            }
        }

        #endregion CRUD

        public override string? ToString()
        {
            return $"ID: {Id}, Name: {Name}, Headquarters: {Headquarters}, DateEstablishment: {DateEstablishment:D}";
        }
    }
}
