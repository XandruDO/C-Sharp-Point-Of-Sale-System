using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace SaleManager
{
    static class MySQLHandler
    {
        /*Changed the Connection String as well as priviledges within MySql Workbench in order to have limited access with user
         * "xandru". Limited to Insert, Update, Delete and NO DROP, no SHUTDOWN, no other critical priviledges.
        */
        public static string connString = "Data Source=localhost; Database=pointofsaledb; uid=xandru; pwd=19661957";
        //public static string connString = "Data Source=localhost; Database=pointofsaledb; uid=root; pwd=password";
        
        public static bool Connected()
        {
            bool connected = false;
            try
            {
                using (MySqlConnection sql_conn = new MySqlConnection(connString))
                {
                    sql_conn.Open();
                }
                connected = true;
            }
            catch { }
            
            return connected;
        }

        public static bool Connected(string _connString)
        {            
            bool connected = false;
            try
            {
                using (MySqlConnection sql_conn = new MySqlConnection(connString))
                {
                    sql_conn.Open();
                }
                connected = true;
            }
            catch { }

            return connected;
        }        

        public static bool ExecuteNonQuery(string cmdText)
        {
            try
            {
                using (MySqlConnection sql_conn = new MySqlConnection(connString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(cmdText, sql_conn))
                    {
                        sql_conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception e) { }

            return false;
        }

        /*public static DataTable GetDataByCmd(string cmdText)
        {
            DataTable ds = new DataTable();

            try
            {
                using (MySqlConnection sql_conn = new MySqlConnection(connString))
                {
                    sql_conn.Open();
                    MySqlDataAdapter sqlda = new MySqlDataAdapter(cmdText, sql_conn);
                    sqlda.Fill(ds);
                }
            }
            catch (Exception fail)
            {
                MessageBox.Show(fail.Message);
                return null;
            }
            return ds;
        }

        public static DataTable GetData(string tableName)
        {
            DataTable dt = new DataTable();
                
            try
            {
                using (MySqlConnection sql_conn = new MySqlConnection(connString))
                {
                    sql_conn.Open();
                    MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT * FROM " + tableName, sql_conn);
                    sqlda.Fill(dt);
                }
            }
            catch (Exception fail)
            {
                MessageBox.Show(fail.Message,"Contact your local administrator!");
                return null;
            }
            return dt;
        }
        */
    }    
}
