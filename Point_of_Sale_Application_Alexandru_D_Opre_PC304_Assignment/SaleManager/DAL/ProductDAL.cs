using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SaleManager
{
    class ProductDAL
    {

        public DataTable GetProductData()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT * FROM tbl_products", conn);
                sqldataAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetProductDataByID(int productID)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT * FROM tbl_products WHERE product_ID = @product_ID", conn);

                sqldataAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetProductDataByOrderID(int orderID)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT tbl_orderdetails.product_id, product_category, product_brand, quantity FROM tbl_orderdetails" + 
                    "left join tbl_products on tbl_orderdetails.product_ID = tbl_products.product_ID where Order_ID=" + orderID.ToString(), conn);
                sqldataAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetCategoryList()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT DISTINCT product_category from tbl_products", conn);
                sqldataAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetProductListByCategory(string category)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT * FROM tbl_products where product_category='" + category + "' and product_stock > 0", conn);
                sqldataAdapter.Fill(dt);
                return dt;
            }
        }

        public DataTable GetProductDataByProductID(int productID)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqldataAdapter = new MySqlDataAdapter("SELECT * from tbl_products WHERE product_ID =" + productID.ToString(), conn);

                sqldataAdapter.Fill(dt);
                return dt;
            }
        }
    }
}
