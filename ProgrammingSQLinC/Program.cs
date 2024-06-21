using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;
using SQL_Console;
using SQLLibrary;

namespace ProgrammingSQLinC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var connStr = "Server = localhost\\sqlexpress01;database=SalesDb;trusted_connection=true;trustServerCertificate=true;";
            Connection sqlConnection = new Connection(connStr);
            sqlConnection.Open();

            CustomerController custCtrl = new CustomerController(sqlConnection);

            var customers = custCtrl.GetAll();

            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id} | Name: {customer.Name}");
            }

            var customer2 = custCtrl.GetByPK(15);
            Console.WriteLine($"ID 15 is {customer2.Name}");

            var test = custCtrl.Search("er");

            foreach (var customer in test)
            {
                Console.WriteLine($"id: {customer.Id} | Name: {customer.Name} | Sales: {customer.Sales}");
            }
            
            //SQLLibrary.Customer newCustomer = new SQLLibrary.Customer //creates a new instance of Customer from the SQLLibrary have to prefix it with 
            ////SQLLibrary because there are two Customer classes one in SQL Console and one in SQLLibrary.
            //{
            //    Id = 0,
            //    Name = "MAX"
            //,
            //    City = "Cincinnati",
            //    State = "OH"
            //,
            //    Sales = 4545,
            //    Active = true
            //};
            //var added = custCtrl.Create(newCustomer);
            //Console.WriteLine("Did the insert succeed?: " + added);
           
            
            
            sqlConnection.Close();


            /*
            //customer2.City = "Lexington";
            //customer2.State = "KY";

            //var changed = custCtrl.Change(customer2);
            //Console.WriteLine("Did the change succeed?: " + changed);


            // customer2 = custCtrl.GetByPK(15);
            //Console.WriteLine($"ID: {customer2.Id} | Name: {customer2.City} | City, State: {customer2.City}, {customer2.State}");

            //var Removed = custCtrl.Remove(37);
            //Console.WriteLine($"did remove succeed?: {Removed}");

            
            

            */

        }
        //static void LearningCode() { 
        //    var ConnStr = "Server = localhost\\sqlexpress01;database=SalesDb;trusted_connection=true;trustServerCertificate=true;";

        //    var Conn = new SqlConnection(ConnStr);//creates and instance of SQLConnection

        //    Conn.Open(); //Opens a connection using the ConnStr that we pass to Conn

        //    if(Conn.State != System.Data.ConnectionState.Open)//checks to make sure connection is open
        //    {
        //        throw new Exception("The Connection didnt open!");
        //    }


        //    Console.WriteLine("Connection Open");

        //    var sql = "SELECT * FROM Customers;";
        //    var sqlcmd = new SqlCommand(sql, Conn);
        //    var reader = sqlcmd.ExecuteReader();



        //    if (!reader.HasRows) // checks to see if the query returned any data.
        //    {
        //        Console.WriteLine("The Customer Returned no rows.");
        //    }

        //    Dictionary<int, Customer> customers = new Dictionary<int, Customer>(); //creates the dictionary int is going to be the key and in this case that is the Id column

        //    while (reader.Read()) //Read() advances the reader to the next record.
        //    {
        //        Customer customer = new Customer();

        //        customer.Id = Convert.ToInt32(reader["Id"]);
        //        customer.Name = Convert.ToString(reader["Name"])!; //ToString method can return a null method but the ! tells vs that it cant be null so remove the warning message
        //        customer.State = Convert.ToString(reader["State"])!;
        //        customer.City = Convert.ToString(reader["City"])!;
        //        customer.Sales = Convert.ToDecimal(reader["Sales"]);
        //        customer.Active = Convert.ToBoolean(reader["Active"]);

        //        customers.Add(customer.Id, customer);


        //        Console.WriteLine($"ID: {customer.Id} | Name: {customer.Name} | Sales: {customer.Sales} | City, State: {customer.City},{customer.State}");
        //    }

        //    reader.Close();

        //    Conn.Close();
        //}
    }
}
