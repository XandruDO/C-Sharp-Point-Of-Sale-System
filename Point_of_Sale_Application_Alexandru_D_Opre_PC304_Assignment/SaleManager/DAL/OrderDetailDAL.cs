using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SaleManager
{
    class OrderDetailDAL
    {

        public bool InsertOrderDetail(int orderID, int productID, int quantity)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "INSERT INTO tbl_orderdetails(Order_ID, Product_ID, Quantity) VALUE(@orderID, @productID, @quantity)"; ;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@orderID", orderID);
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch {
                        MessageBox.Show("Insert not possible. Please try again or contact your system administrator.");
                    }

                    return false;
                }
            }
        }

        public bool UpdateStock(int productID, int quantity)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand command = new MySqlCommand())
                {
                    command.Connection = conn;
                    command.CommandText = "Update tbl_products set Product_Stock = Product_Stock - @quantity where Product_ID = @productID"; ;
                    command.CommandType = CommandType.Text;
                    command.Parameters.AddWithValue("@productID", productID);
                    command.Parameters.AddWithValue("@quantity", quantity);

                    try
                    {
                        conn.Open();
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch {

                    }

                    return false;
                }
            }
        }
    }
}
