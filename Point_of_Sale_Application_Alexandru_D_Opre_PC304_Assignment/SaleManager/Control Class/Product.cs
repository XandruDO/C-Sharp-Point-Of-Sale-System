using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace SaleManager
{
    class Product
    {
        public int ID { get; set; }
        public string Brand;
        public string Category;
        public string SKU;
        public double Price;
        public int Stock;

        ProductDAL proDAL = new ProductDAL();

        public Product() { }

        public Product(int _product_ID, double _price, int _stock)
        {
            ID = _product_ID;
            Price = _price;
            Stock = _stock;
        }
        
        public Product(int _productID, double _price, string _category, string _brand, int _stock)
        {
            Brand = _brand;
            Category = _category;
            ID = _productID;
            Price = _price;
            Stock = _stock;
        }

        public DataTable GetProductData()
        {
            return proDAL.GetProductData();
        }

        public DataTable GetProductDataByOrderID(int orderID)
        {
            return proDAL.GetProductDataByOrderID(orderID);
        }

        public DataTable GetCategoryList()
        {
            return proDAL.GetCategoryList();
        }

        public DataTable GetProductListByCategory(string category)
        {
            return proDAL.GetProductListByCategory(category);
        }

        public DataTable GetProductListByProductID(int productID)
        {
            return proDAL.GetProductDataByProductID(productID);
        }
    }
}
