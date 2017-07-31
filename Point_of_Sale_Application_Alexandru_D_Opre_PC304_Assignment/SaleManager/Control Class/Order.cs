using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SaleManager
{
    class Order
    {
        int OrderID;
        int CustomerID;
        int EmployeeID;
        DateTime TimeOfSale;
        double TotalPrice;
        OrderDAL ordDAL = new OrderDAL();

        public Order(int _OrderID)
        {
            OrderID = _OrderID;
        }

        public Order(int _customerID, int _employeeID, DateTime _timeOfSale, double _totalPrice) 
        {
            CustomerID = _customerID;
            EmployeeID = _employeeID;
            TimeOfSale = _timeOfSale;
            TotalPrice = _totalPrice;
        }

        public Order(int _orderID, int _customerID, int _employeeID, DateTime _timeOfSale)
        {
            OrderID = _orderID;
            CustomerID = _customerID;
            EmployeeID = _employeeID;
            TimeOfSale = _timeOfSale;
        }

        public DataTable GetOrderData()
        {
            return ordDAL.GetOrderData();
        }

        public DataTable GetOrderDataFullName()
        {
            return ordDAL.GetOrderDataFullName();
        }

        public void Insert(List<OrderDetail> myList)
        {/*When this method executes, for each item in the orderDetail list
         //the method executes both Insert() and UpdateStock()
         //inserting new information into the database based on these parameters
         in both the Orders table and the OrderDetails table.
         */
            int orderID = ordDAL.InsertOrder(CustomerID, EmployeeID, TimeOfSale, TotalPrice);
            foreach(OrderDetail orderDetail in myList)
            {
                orderDetail.OrderID = orderID;
                orderDetail.Insert();
                orderDetail.UpdateStock();
            }            
        }

        public void Update()
        {
            ordDAL.UpdateOrder(OrderID, CustomerID, EmployeeID, TimeOfSale);
        }

        public void Delete()
        {//This method deletes data from both the Orders table and the OrderDetails table
         //based on OrderID.
            ordDAL.UpdateStockBeforeDeleteOrder(OrderID);
            ordDAL.DeleteOrder(OrderID);
            ordDAL.DeleteOrderDetailByOrderID(OrderID);
            
        }

        
    }
}
