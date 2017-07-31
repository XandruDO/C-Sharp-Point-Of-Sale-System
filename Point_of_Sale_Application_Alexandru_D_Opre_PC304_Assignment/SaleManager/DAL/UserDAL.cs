using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace SaleManager
{
    class UserDAL
    {
        public int GetLoginID(string userName, string password, string role)
        {
            int id;
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "SELECT loginID FROM tbl_login WHERE username = @username and password = @password and role = @role LIMIT 1";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("username", userName);
                    comm.Parameters.AddWithValue("password", password);
                    comm.Parameters.AddWithValue("role", role);

                    conn.Open();
                    using (MySqlDataReader reader = comm.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = int.Parse(reader.GetString(0));
                        }
                        else
                        {
                            id = -1;
                        }
                    }
                }
            }
            return id;
        }

        public bool IsValidUser(string userName, string password, string role)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "SELECT count(*) FROM tbl_login WHERE username = @username and password = @password and role = @role";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("username", userName);
                    comm.Parameters.AddWithValue("password", password);
                    comm.Parameters.AddWithValue("role", role);

                    try
                    {
                        conn.Open();
                        int totalCount = Convert.ToInt32(comm.ExecuteScalar());
                        if (totalCount > 0)
                            return true;
                    }
                    catch { }

                    return false;
                }
            }
        }
    }
}
