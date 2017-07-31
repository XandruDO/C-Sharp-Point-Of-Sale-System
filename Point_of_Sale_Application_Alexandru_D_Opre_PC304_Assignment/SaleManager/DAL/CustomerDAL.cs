using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SaleManager
{
    class CustomerDAL
    {

        public DataTable GetCustomerData()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT * FROM tbl_customers left join tbl_customers_address on tbl_customers.customer_ID = tbl_customers_address.customer_ID", conn);
                sqlda.Fill(dt);
                return dt;
            }
        }

        public DataTable GetCustomerDataFullName()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT Customer_ID, CONCAT(First_Name, ' ', Second_Name) as Full_Name FROM tbl_customers", conn);
                sqlda.Fill(dt);
                return dt;
            }
        }

        public int InsertCustomer(string firstName, string secondName)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "INSERT INTO tbl_customers(first_Name, second_Name) VALUE(@firstName, @secondName)"; ;
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@firstName", firstName);
                    comm.Parameters.AddWithValue("@secondName", secondName);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return (int)comm.LastInsertedId;
                    }
                    catch { }

                    return -1;
                }
            }
        }

        public bool UpdateCustomer(int customerID, string firstName, string secondName)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE tbl_customers SET first_Name = @firstName, second_Name = @secondName WHERE customer_ID = @customerID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@firstName", firstName);
                    comm.Parameters.AddWithValue("@secondName", secondName);
                    comm.Parameters.AddWithValue("@customerID", customerID);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return true;
                    }
                    catch { }

                    return false;
                }
            }
        }

        public bool DeleteCustomer(int customerID)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM tbl_customers WHERE customer_ID = @customerID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@customerID", customerID);

                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                        return true;
                    }
                    catch { }

                    return false;
                }
            }
        }
    }
}
