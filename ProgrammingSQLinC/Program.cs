

using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace ProgrammingSQLinC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ConnStr = "Server = localhost\\sqlexpress01;database=SalesDb;trusted_connection=true;trustServerCertificate=true;";

            var Conn = new SqlConnection(ConnStr);//creates and instance of SQLConnection

            Conn.Open(); //Opens a connection using the ConnStr that we pass to Conn

            if(Conn.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("The Connection didnt open!");
            }

            Console.WriteLine("Connection Open");

            var sql = "SELECT * FROM Customers;";
            var sqlcmd = new SqlCommand(sql, Conn);
            var reader = sqlcmd.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine("The Customer Returned no rows.");
            }

            while (reader.Read()) //Read() advances the reader to the next record.
            {
                var id = Convert.ToInt32(reader["Id"]);
                var name = Convert.ToString(reader["Name"]);
                var sales = Convert.ToDecimal(reader["Sales"]);
                Console.WriteLine($"ID: {id} | Name: {name} | Sales: {sales}");
            }

            reader.Close();

            Conn.Close();
        }
    }
}
