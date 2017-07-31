//Code written by Alexandru D Opre. Copyright Alexandru Daniel Opre.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SaleManager
{
    class OrderDAL
    {
        //Order
        public DataTable GetOrderData()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT * FROM tbl_Orders" + "left join tbl_products on tbl_orders.product_id = tbl_products.product_id", conn);
                sqlda.Fill(dt);
                return dt;
            }
        }

        public DataTable GetOrderDataFullName()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
          MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT Order_ID, tbl_orders.Employee_ID,  tbl_orders.Customer_ID, CONCAT(tbl_Employees.First_Name, ' ', tbl_Employees.Last_Name) as Employee,"
                + " CONCAT(tbl_customers.First_Name, ' ', tbl_customers.Second_Name) as Customer, Time_Of_Sale, TotalPrice FROM tbl_orders "
                + " left join tbl_Employees on tbl_orders.Employee_ID = tbl_Employees.EmployeeID"
                + " left join tbl_customers on tbl_orders.Customer_ID = tbl_customers.Customer_ID", conn);
                sqldataAdapter.Fill(dt);
                return dt;
            }
        }

        public int InsertOrder(int customerID, int employeeID, DateTime timeOfSale, double totalPrice)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "INSERT INTO tbl_Orders(Customer_ID, Employee_ID, Time_Of_Sale, TotalPrice) VALUE(@customerID, @employeeID, @timeOfSale, @totalPrice)"; ;
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@customerID", customerID);
                    comm.Parameters.AddWithValue("@employeeID", employeeID);
                    comm.Parameters.AddWithValue("@timeOfSale", timeOfSale);
                    comm.Parameters.AddWithValue("@totalPrice", totalPrice);

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

        public bool UpdateOrder(int orderID, int customerID, int employeeID, DateTime timeOfSale)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE tbl_Orders SET Customer_ID = @customerID, Employee_ID = @employeeID, Time_Of_Sale = @timeOfSale WHERE Order_ID = @orderID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@customerID", customerID);
                    comm.Parameters.AddWithValue("@employeeID", employeeID);
                    comm.Parameters.AddWithValue("@timeOfSale", timeOfSale);
                    comm.Parameters.AddWithValue("@orderID", orderID);

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

        public bool DeleteOrder(int orderID)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM tbl_Orders WHERE Order_ID = @orderID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@orderID", orderID);

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

        public bool DeleteOrderDetailByOrderID(int orderID)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM tbl_OrderDetails WHERE Order_ID = @orderID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@orderID", orderID);

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

        public bool UpdateStockBeforeDeleteOrder(int orderID)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE tbl_products join tbl_orderdetails on tbl_products.product_ID = tbl_orderdetails.product_ID"
                    + " set product_stock = product_stock + quantity WHERE order_ID = @orderID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@orderID", orderID);

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
