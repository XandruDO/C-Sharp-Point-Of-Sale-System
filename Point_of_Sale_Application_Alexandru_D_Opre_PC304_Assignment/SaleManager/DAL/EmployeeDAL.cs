using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace SaleManager
{
    class EmployeeDAL
    {
    
        public DataTable GetEmployeeData()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT * FROM tbl_employees", conn);
                sqlda.Fill(dt);
                return dt;
            }
        }

        public DataTable GetEmployeeDataFullName()
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {  //This selects the employee ID, the first name and last name and concatenates them into Full_Name
                MySqlDataAdapter sqlda = new MySqlDataAdapter("SELECT EmployeeID, CONCAT(First_Name, ' ', Last_Name) as Full_Name FROM tbl_employees", conn);
                sqlda.Fill(dt);
                return dt;
            }
        }

        public bool InsertEmployee(int _loginID, string _firstName, string _lastName, string _addressStreet, string _addressPostCode, string _addressCity, string _role)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "INSERT INTO tbl_Employees(LoginID, First_Name, Last_Name, Address_Street, Address_Postcode, Address_City, Role) VALUE(@LoginID, @First_Name, @Last_Name, @Address_Street, @Address_Postcode, @Address_City, @Role)"; ;
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@LoginID", _loginID);
                    comm.Parameters.AddWithValue("@First_Name", _firstName);
                    comm.Parameters.AddWithValue("@Last_Name", _lastName);
                    comm.Parameters.AddWithValue("@Address_Street", _addressStreet);
                    comm.Parameters.AddWithValue("@Address_Postcode", _addressPostCode);
                    comm.Parameters.AddWithValue("@Address_City", _addressCity);
                    comm.Parameters.AddWithValue("@Role", _role);

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

        public bool UpdateEmployee(int _employeeID, int _loginID, string _firstName, string _lastName, string _addressStreet, string _addressPostCode, string _addressCity, string _role)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE tbl_Employees SET LoginID = @LoginID, First_Name = @First_Name, Last_Name = @Last_Name, Address_Street = @Address_Street, Address_Postcode = @Address_Postcode, Address_City = @Address_City, Role = @Role WHERE EmployeeID = @EmployeeID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@EmployeeID", _employeeID);
                    comm.Parameters.AddWithValue("@LoginID", _loginID);
                    comm.Parameters.AddWithValue("@First_Name", _firstName);
                    comm.Parameters.AddWithValue("@Last_Name", _lastName);
                    comm.Parameters.AddWithValue("@Address_Street", _addressStreet);
                    comm.Parameters.AddWithValue("@Address_Postcode", _addressPostCode);
                    comm.Parameters.AddWithValue("@Address_City", _addressCity);
                    comm.Parameters.AddWithValue("@Role", _role);

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

        public bool DeleteEmployee(int EmployeeID)
        {
            using (MySqlConnection conn = new MySqlConnection(MySQLHandler.connString))
            {
                using (MySqlCommand comm = new MySqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM tbl_Employees WHERE Employee_ID = @EmployeeID";
                    comm.CommandType = CommandType.Text;
                    comm.Parameters.AddWithValue("@EmployeeID", EmployeeID);

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
