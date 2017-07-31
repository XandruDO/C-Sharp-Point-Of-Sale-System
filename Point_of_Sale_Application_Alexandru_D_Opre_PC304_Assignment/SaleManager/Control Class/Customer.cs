using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace SaleManager
{
    class Customer
    {
        int CustomerID;
        string FirstName;
        string SecondName;
        CustomerDAL cusDAL = new CustomerDAL();

        public Customer() { }

        public Customer(int _customerID)
        {
            CustomerID = _customerID;
        }

        public Customer(string _firstName, string _secondName) 
        {
            FirstName = _firstName;
            SecondName = _secondName;
        }

        public Customer(int _customerID, string _firstName, string _secondName)
        {
            CustomerID = _customerID;
            FirstName = _firstName;
            SecondName = _secondName;
        }

        public DataTable GetCustomerData()
        {
            return cusDAL.GetCustomerData();
        }

        public DataTable GetCustomerDataFullName()
        {
            return cusDAL.GetCustomerDataFullName();
        }

        
        public void Update()
        {
            cusDAL.UpdateCustomer(CustomerID, FirstName, SecondName);
        }

        public void Delete()
        {
            cusDAL.DeleteCustomer(CustomerID);
        }

        public int Insert()
        {
            return cusDAL.InsertCustomer(FirstName, SecondName);
        }
    }
}
