using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;

namespace SQLLibrary
{
    public class CustomerController
    {
        private SqlConnection? _Sqlconnection { get; set; } = null;
        private static string SqlGetAll = "SELECT * FROM Customers;"; //sql command in a string format also saved it to a parameter so that it could
                                                                      // easily be accessed by more than one method
        public List<Customer> GetAll() //says its going to return a list doesnt make a list
        {
            var sql = SqlGetAll; //getting sql query from stored procedure more effecient and easier to update due to all of the sql being in one spot.
            var sqlcmd = new SqlCommand(sql, _Sqlconnection); //makes it a sql command and tells it were to run it at
            var reader = sqlcmd.ExecuteReader(); //excutes the sqlcmd and saves it to a variable

            List<Customer> customers = new List<Customer>(); //initialzes the list that we will return

            while (reader.Read()) //starts the loop to iterate through the excuteded commands results
            {
                Customer customer = new Customer(); //creates an instance of the customer type so we can access the properties under customer to pass 
                                                    // the results of the reader to the properties in the Customer class

                ConvertToCustomer(customer, reader);

                customers.Add(customer); //adds customer to the list

            }
            reader.Close();

            return customers;



        }
        public Customer? GetByPK(int custId) // ? by Customer means it can be null
        {
            var sql = $"SELECT * FROM Customers Where Id = {custId};";
            var sqlcmd = new SqlCommand(sql, _Sqlconnection);

            var reader = sqlcmd.ExecuteReader();

            if (!reader.HasRows)
            {
                reader.Close();
                return null;
            }
            reader.Read();
            Customer customer = new Customer();

            ConvertToCustomer(customer, reader);

            reader.Close();
            return customer;
        }
        public bool Create(Customer customer)
        {
            //var sql = $" INSERT Customers (Name,City,State,Sales,Active) " +
            //            $"VALUES ('{customer.Name}','{customer.City}','{customer.State}',{customer.Sales},{(customer.Active ? 1 : 0)})";
            var sql = $" INSERT Customers ( Name, City, State, Sales, Active) VALUES " +
                        "(@Name, @City, @State, @Sales, @Active);";
            var sqlcmd = new SqlCommand(sql, _Sqlconnection);

            CustomerSqlPara(sqlcmd, customer);

            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return rowsAffected == 1 ? true : false; //makes sure the customer was created
        }
        public bool Change(Customer customer)
        {
            //var sql = $"Update Customers Set Name = {customer.Name}, City = {customer.City}, State = {customer.State}, Sales = {customer.Sales}, Active = {customer.Active} where Id = {customer.Id} ";
            var sql = $"UPDATE Customers SET " +
                        $"Name = @Name, City = @City, State = @State, Sales = @Sales, Active = @Active Where Id = @Id;";
            
            var sqlcmd = new SqlCommand(sql, _Sqlconnection);

            sqlcmd.Parameters.AddWithValue("@Id", customer.Id); //Pass the values from customer to the matching Parameters in the sql query.
            
            CustomerSqlPara(sqlcmd,customer);


            var rowsAffected = sqlcmd.ExecuteNonQuery();

            return rowsAffected == 1 ? true : false;

        }
        public bool Remove(int id) 
        {
            var sql = $"DELETE Customers WHERE Id = @Id ";
            var sqlcmd = new SqlCommand(sql, _Sqlconnection);
            Customer customer = new Customer();
            sqlcmd.Parameters.AddWithValue("@Id", customer.Id);
            var didRemove = sqlcmd.ExecuteNonQuery();

            return didRemove == 1 ? true : false;

        }
        public List<Customer> Search(string name)
        {
            
            var sql = $"Select * From Customers Where Name Like '%{name}%' ";
            var sqlcmd = new SqlCommand(sql, _Sqlconnection);



            var reader = sqlcmd.ExecuteReader();
            List<Customer> customers = new List<Customer>();

            while(reader.Read())
            {
                Customer customer = new Customer();
                ConvertToCustomer(customer,reader);

                customers.Add(customer); //adds customer to the list

            }
            reader.Close();
            return customers;

        }
        private void ConvertToCustomer(Customer customer, SqlDataReader reader)
        {

            customer.Id = Convert.ToInt32(reader["Id"]); //saves the result from the reader to the property that it matches in the customer type
            customer.Name = Convert.ToString(reader["Name"])!;
            customer.City = Convert.ToString(reader["City"])!;
            customer.State = Convert.ToString(reader["State"])!;
            customer.Sales = Convert.ToDecimal(reader["Sales"]);
            customer.Active = Convert.ToBoolean(reader["Active"]);

        }//refactoring
        private void CustomerSqlPara(SqlCommand sqlcmd,Customer customer)
        {
            sqlcmd.Parameters.AddWithValue("@Name", customer.Name);
            sqlcmd.Parameters.AddWithValue("@City", customer.City);
            sqlcmd.Parameters.AddWithValue("@State", customer.State);
            sqlcmd.Parameters.AddWithValue("@Sales", customer.Sales);
            sqlcmd.Parameters.AddWithValue("@Active", customer.Active);
        }
        public CustomerController(Connection connection) ///this is connecting what is passed from
            ///program as a CustomerController type to CustomerController and it takes it and passes
            ///it to Connection which and then this makes sure there is an open connection then enables it
        {
            if (connection.GetSqlConnection() != null)
            {
                _Sqlconnection = connection.GetSqlConnection()!;
            }
        }


    }
}
