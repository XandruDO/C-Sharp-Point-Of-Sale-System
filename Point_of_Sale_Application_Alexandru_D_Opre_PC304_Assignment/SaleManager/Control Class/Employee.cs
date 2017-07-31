using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SaleManager
{
    class Employee
    {
        int EmployeeID;
        int LoginID;
        string First_Name;
        string Last_Name;
        string Address_Street;
        string Address_Postcode;
        string Address_City;
        string Role;

        EmployeeDAL empDAL = new EmployeeDAL();

        public Employee() { }

        public Employee(int _employeeID) 
        {
            EmployeeID = _employeeID;
        }

        public Employee(int _loginID, string _firstName, string _lastName, string _addressStreet, string _addressPostCode, string _addressCity, string _role)
        {
            LoginID = _loginID;
            First_Name = _firstName;
            Last_Name = _lastName;
            Address_Street = _addressStreet;
            Address_Postcode = _addressPostCode;
            Address_City = _addressCity;
            Role = _role;
        }

        public Employee(int _employeeID, int _loginID, string _firstName, string _lastName, string _addressStreet, string _addressPostCode, string _addressCity, string _role)
        {
            EmployeeID = _employeeID;
            LoginID = _loginID;
            First_Name = _firstName;
            Last_Name = _lastName;
            Address_Street = _addressStreet;
            Address_Postcode = _addressPostCode;
            Address_City = _addressCity;
            Role = _role;
        }

        public DataTable GetEmployeeData()
        {
            return empDAL.GetEmployeeData();
        }

        public DataTable GetEmployeeDataFullName()
        {
            return empDAL.GetEmployeeDataFullName();
        }

        public void Insert()
        {
            empDAL.InsertEmployee(LoginID, First_Name, Last_Name, Address_Street, Address_Postcode, Address_City, Role);
        }

        public void Update()
        {
            empDAL.UpdateEmployee(EmployeeID, LoginID, First_Name, Last_Name, Address_Street, Address_Postcode, Address_City, Role);
        }

        public void Delete()
        {
            empDAL.DeleteEmployee(EmployeeID);
        }
    }
}
