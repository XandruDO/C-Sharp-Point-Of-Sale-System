using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;


namespace SaleManager
{
    class CustomerAddressDAL
    {

        
        public DataTable GetCustomerAddressData()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT * FROM tbl_customers_address", conn);
                sqlda.Fill(dt);
                return dt;
            }
        }

        public bool InsertCustomerAddress(int customerID, string address, string city, string county, string postCode)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "INSERT INTO tbl_customers_address (customer_ID, address, city, county, postcode) VALUE(@customerID, @address, @city, @county, @postCode);"; 
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@address", address);
                    comm.Parameters.AddWithValue("@city", city);
                    comm.Parameters.AddWithValue("@county", county);
                    comm.Parameters.AddWithValue("@postcode", postCode);
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

        public bool UpdateCustomerAddress(int customerID, string address, string city, string county, string postCode)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE tbl_customers_address SET address = @address, city = @city, county = @county, postCode = @postcode WHERE customer_ID = @customerID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@address", address);
                    comm.Parameters.AddWithValue("@city", city);
                    comm.Parameters.AddWithValue("@county", county);
                    comm.Parameters.AddWithValue("@postcode", postCode);
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
