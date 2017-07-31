using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace SaleManager
{
    
    class CustomerAddress
    {
        int customerID;
        string address;
        string city;
        string county;
        string postCode;
        CustomerAddressDAL cusAddressDal = new CustomerAddressDAL();

        public CustomerAddress() { }

        public CustomerAddress(int _customerID)
        {
            customerID = _customerID;
        }

        public CustomerAddress(string _city, string _county, string _postcode)
        {
            city = _city;
            county = _county;
            postCode = _postcode;
        }

        public CustomerAddress(int _customerID, string _address, string _city, string _county, string _postcode)
        {
            customerID = _customerID;
            address = _address;
            city = _city;
            county = _county;
            postCode = _postcode;
        }

        public void Insert()
        {
            cusAddressDal.InsertCustomerAddress(customerID, address, city, county, postCode);
        }

        public void Update()
        {
            cusAddressDal.UpdateCustomerAddress(customerID, address, city, county, postCode);
        }
    }
}
