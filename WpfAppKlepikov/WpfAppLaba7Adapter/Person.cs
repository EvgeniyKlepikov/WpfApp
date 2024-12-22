using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLaba7Adapter
{
    public class Person
    {
        static string connectionString;
        static SqlConnection connection;
        static SqlDataAdapter adapter;
        static DataTable personTable = new DataTable();
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }
        public decimal Salary { get; set; }
        public static void newConnection()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            connection = new SqlConnection(connectionString);
            connection.Open();
        }
        public static DataTable ViewAll()
        {
            newConnection();
            string sql = "SELECT * FROM Person";
            adapter = new SqlDataAdapter(sql, connection);
            adapter.Fill(personTable);
            connection.Close();
            return personTable;
        }
        public static void Update()
        {
            newConnection();
            // commandBuilder позволяет автоматически сгенерировать нужные SQL-выражения 
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(personTable);
            connection.Close();

        }
        public string Find()
        {
            // для каждого варианта поиска требуется отдельный SELECT
            newConnection();
            DataTable personTable1 = new DataTable();
            string str = "";
            if (Name != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE (Name=@name)", connection);
                command.Parameters.AddWithValue("name", Name);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(personTable1);
                foreach (DataRow row in personTable1.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            }
            else if (Company != null)
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Person WHERE (Company=@company)", connection);
                command.Parameters.AddWithValue("company", Company);
                adapter = new SqlDataAdapter(command);

                adapter.Fill(personTable1);
                foreach (DataRow row in personTable1.Rows)
                {
                    // получаем все ячейки строки
                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        str += $"\t{cell}";
                    str += "\n";
                }

                return str;
            }
            return null;

        }
        public override string ToString()
        {
            return $"{ID} {Name}, {Age} лет. Сотрудник {Company}, оклад {Salary}";
        }

    }
}
