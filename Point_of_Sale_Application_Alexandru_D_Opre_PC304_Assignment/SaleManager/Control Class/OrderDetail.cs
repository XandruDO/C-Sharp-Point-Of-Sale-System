using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager
{
    class OrderDetail
    {
        public int ID;
        public int OrderID;
        public int ProductID { get; set; }        
        public string ProductBrand { get; set; }
        public int Quantity { get; set; }
        OrderDetailDAL orderDetailDAL = new OrderDetailDAL();

        //public OrderDetail(int _orderID, int _productID, int _quantity)
        //{
        //    OrderID = _orderID;
        //    ProductID = _productID;
        //    Quantity = _quantity;
        //}

        public OrderDetail(int _productID, string _productBrand, int _quantity)
        {
            ProductID = _productID;
            ProductBrand = _productBrand;
            Quantity = _quantity;
        }

        public bool Insert()
        {
            return orderDetailDAL.InsertOrderDetail(OrderID, ProductID, Quantity);
        }

        public void UpdateStock()
        {
            orderDetailDAL.UpdateStock(ProductID, Quantity);
        }
    }
}
