/* Author: Alexandru Daniel Opre
 * ID: 41982
 * */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SaleManager
{
    public partial class Form1 : Form
    {
        int loginID;
        string role;
        List<OrderDetail> orderDetailList;
        List<Product> productPriceDic = new List<Product>();
        double totalPrice = 0;

        public Form1(int _loginID, string _role)
        {
            InitializeComponent();
            tcSaleManager.SelectedIndex = 0;

            loginID = _loginID;
            role = _role;

            if (_role == "SaleStaff")
            {
                tcSaleManager.TabPages.Remove(tabEmployee);
            }
            else
            {
                LoadCustomerData();
                LoadEmployeeData();
            }

            LoadOrderData();
            
        }


        //======================== Customer part of the form
        #region Customer

        private void LoadCustomerData()
        {
            LoadCustomerGridViewData();

            tlpCustomer.Enabled = false;
            btnUpdateCustomer.Enabled = false;
            btnDeleteCustomer.Enabled = false;
        }

        private void LoadCustomerGridViewData()
        {
            CustomerDAL cusDAL = new CustomerDAL();
            CustomerAddressDAL cusAddressDAL = new CustomerAddressDAL();
            DataTable dt = cusDAL.GetCustomerData();
            
            if (dt.Rows.Count > 0)
            {
                dgCustomer.DataSource = dt;
                dgCustomer.Columns["Customer_ID"].Visible = false;

            }
            else
            {
                dgCustomer.DataSource = null;
            }
        }

        private void ClearCustomerInputFields()
        {
            txtCustomerFirstName.Text = "";
            txtCustomerSecondName.Text = "";
            txtCusAddress.Text = "";
            txtCusCity.Text = "";
            txtCusCounty.Text = "";
            txtCusPostcode.Text = "";
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (btnAddCustomer.Text == "New")
            {
                tlpCustomer.Enabled = true;
                btnAddCustomer.Text = "Add";
                ClearCustomerInputFields();
                btnUpdateCustomer.Enabled = false;
                btnDeleteCustomer.Enabled = false;
            }
            else
            {
                tlpCustomer.Enabled = false;
                btnAddCustomer.Text = "New";
                btnUpdateCustomer.Enabled = false;
                btnDeleteCustomer.Enabled = false;

                try
                {
                    Customer customer = new Customer(txtCustomerFirstName.Text, txtCustomerSecondName.Text);
                    int customerID = customer.Insert();
                    CustomerAddress customerAddress = new CustomerAddress(customerID, txtCusAddress.Text, txtCusCity.Text, txtCusCounty.Text, txtCusPostcode.Text);
                    customerAddress.Insert();
                    ClearCustomerInputFields();
                    LoadCustomerGridViewData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Add operation failed! Please contact your admin.", "Notice");
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnUpdateCustomer_Click(object sender, EventArgs e)
        {
            btnAddCustomer.Enabled = true;
            btnUpdateCustomer.Enabled = false;
            btnDeleteCustomer.Enabled = false;
            tlpCustomer.Enabled = false;

            try
            {
                Customer customer = new Customer(int.Parse(txtCustomerID.Text), txtCustomerFirstName.Text, txtCustomerSecondName.Text);
                CustomerAddress customerAddress = new CustomerAddress(int.Parse(txtCustomerID.Text), txtCusAddress.Text, txtCusCity.Text, txtCusCounty.Text, txtCusPostcode.Text);
                customerAddress.Update();
                customer.Update();
                ClearCustomerInputFields();
                LoadCustomerGridViewData();
            }
            catch (Exception)
            {
                MessageBox.Show("Update operation failed! Please contact your admin.", "Notice");
            }
        }

        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            ClearCustomerInputFields();
            btnAddCustomer.Enabled = true;
            btnUpdateCustomer.Enabled = false;
            btnDeleteCustomer.Enabled = false;
            tlpCustomer.Enabled = false;
            try
            {
                Customer customer = new Customer(int.Parse(txtCustomerID.Text));
                customer.Delete();
                ClearCustomerInputFields();
                LoadCustomerGridViewData();
            }
            catch (Exception)
            {
                MessageBox.Show("Delete operation failed! Please contact your admin.", "Notice");
            }
        }

        private void btnCancelCustomer_Click(object sender, EventArgs e)
        {
            btnAddCustomer.Text = "New";
            btnAddCustomer.Enabled = true;
            ClearCustomerInputFields();
            btnUpdateCustomer.Enabled = false;
            btnDeleteCustomer.Enabled = false;
            tlpCustomer.Enabled = false;
        }

        private void dgCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAddCustomer.Text = "New";
                btnAddCustomer.Enabled = false;
                btnUpdateCustomer.Enabled = true;
                btnDeleteCustomer.Enabled = true;
                tlpCustomer.Enabled = true;
                //This tells each textbox what value to take based on which cell clicked.
                txtCustomerID.Text = dgCustomer.Rows[e.RowIndex].Cells["Customer_ID"].Value.ToString();
                txtCustomerFirstName.Text = dgCustomer.Rows[e.RowIndex].Cells["First_Name"].Value.ToString();
                txtCustomerSecondName.Text = dgCustomer.Rows[e.RowIndex].Cells["Second_Name"].Value.ToString();
                txtCusAddress.Text = dgCustomer.Rows[e.RowIndex].Cells["address"].Value.ToString();
                txtCusCity.Text = dgCustomer.Rows[e.RowIndex].Cells["city"].Value.ToString();
                txtCusCounty.Text = dgCustomer.Rows[e.RowIndex].Cells["county"].Value.ToString();
                txtCusPostcode.Text = dgCustomer.Rows[e.RowIndex].Cells["postcode"].Value.ToString();
                //This loop basically colors the background and the foreground color for each cell;
                for (int i = 0; i < dgCustomer.RowCount; i++)
                { 
                    dgCustomer.Rows[i].DefaultCellStyle.BackColor = Color.White;
                    dgCustomer.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }
                //This code colors the current row, the one that's been clicked on, to differentiate it from the rest 
                //and show the user where he (last) clicked.
                dgCustomer.CurrentRow.DefaultCellStyle.BackColor = Color.FromArgb(140, 18, 116);
                dgCustomer.CurrentRow.DefaultCellStyle.ForeColor = Color.White;
            }
            catch (Exception)
            {

            }
        }

        #endregion Customer

        //===================== Order part of the form
        #region Order

        private void LoadOrderData()
        {
            Employee emp = new Employee();
            DataTable dt = emp.GetEmployeeDataFullName();
            if (dt.Rows.Count > 0)
            {
                cbbEmployeeList.DataSource = dt;
                cbbEmployeeList.DisplayMember = "Full_Name";
                cbbEmployeeList.ValueMember = "EmployeeID";
                cbbEmployeeList.SelectedIndex = 0;
            }

            Customer cus = new Customer();
            dt = cus.GetCustomerDataFullName();
            if (dt.Rows.Count > 0)
            {
                cbbCustomerList.DataSource = dt;
                cbbCustomerList.DisplayMember = "Full_Name";
                cbbCustomerList.ValueMember = "Customer_ID";
                cbbCustomerList.SelectedIndex = 0;
            }

            Product pro = new Product();

            dt = pro.GetCategoryList();
            if (dt.Rows.Count > 0)
            {
                cbbProductCategory.DataSource = dt;
                cbbProductCategory.DisplayMember = "product_category";
                cbbProductCategory.ValueMember = "product_category";
                cbbProductCategory.SelectedIndex = 0;
            }

            LoadOrderGridViewData();

            tlpOrder.Enabled = false;
            btnUpdateOrder.Enabled = false;
            btnDeleteOrder.Enabled = false;
        }

        private void LoadOrderGridViewData()
        {
            OrderDAL ordDAL = new OrderDAL();
            DataTable dt = ordDAL.GetOrderDataFullName();
            if (dt.Rows.Count > 0)
            {
                dgOrder.DataSource = dt;
                dgOrder.Columns["Order_ID"].Visible = true;
                dgOrder.Columns["Customer_ID"].Visible = true;
                dgOrder.Columns["Employee_ID"].Visible = true;
            }
            else
                dgOrder.DataSource = null;
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            if (btnAddOrder.Text == "New")
            {
                tlpOrder.Enabled = true;
                btnAddOrder.Text = "Add";
                btnUpdateOrder.Enabled = false;
                btnDeleteOrder.Enabled = false;
                dtpTimeOfSale.Enabled = false;

                dgCart.DataSource = null;
                //Automatically setting the quantity to a minimum of 1
                txtQuantity.Text = "1";
                orderDetailList = new List<OrderDetail>();
            }
            else
            {
                tlpOrder.Enabled = false;
                btnAddOrder.Text = "New";
                btnUpdateOrder.Enabled = false;
                btnDeleteOrder.Enabled = false;
                dtpTimeOfSale.Enabled = false;

                try
                {
                    Order Order = new Order((int)cbbCustomerList.SelectedValue, (int)cbbEmployeeList.SelectedValue, DateTime.Now, totalPrice);
                    Order.Insert(orderDetailList);

                    LoadOrderGridViewData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Adding the order failed! Contact your local administrator");
                }
            }
        }

        private void btnUpdateOrder_Click(object sender, EventArgs e)
        {
            btnAddOrder.Enabled = true;
            btnUpdateOrder.Enabled = false;
            btnDeleteOrder.Enabled = false;
            tlpOrder.Enabled = false;

            try
            {
                Order Order = new Order(int.Parse(txtOrderID.Text), (int)cbbCustomerList.SelectedValue, (int)cbbEmployeeList.SelectedValue, dtpTimeOfSale.Value);
                Order.Update();
                LoadOrderGridViewData();
            }
            catch (Exception)
            {
                MessageBox.Show("Updating the order failed! Contact your local administrator");
            }
        }

        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {

            btnAddOrder.Enabled = true;
            btnUpdateOrder.Enabled = false;
            btnDeleteOrder.Enabled = false;
            tlpOrder.Enabled = false;
            try
            {
                Order order = new Order(int.Parse(txtOrderID.Text));
                order.Delete();
                LoadOrderGridViewData();
                dgCart.DataSource = null;
                lblTotalPrice.Text = "Total price: 0";
            }
            catch (Exception)
            {
                MessageBox.Show("Deleting the order failed! Contact your local administrator");
            }
        }

        private void btnCancelOrder_Click(object sender, EventArgs e)
        {//Reverses the text on the Add button to "New" again.
            btnAddOrder.Text = "New";
            btnAddOrder.Enabled = true;


            btnUpdateOrder.Enabled = false;
            btnDeleteOrder.Enabled = false;
            tlpOrder.Enabled = false;
            lblTotalPrice.Text = "0.00";

            cbbProductCategory.Enabled = true;
            cbbProducts.Enabled = true;
            txtQuantity.Enabled = true;
            btnAddtoCart.Enabled = true;
            dgCart.Enabled = true;

        }

        private void dgOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAddOrder.Text = "New";
                btnAddOrder.Enabled = false;
                btnUpdateOrder.Enabled = true;
                btnDeleteOrder.Enabled = true;
                tlpOrder.Enabled = true;

                cbbProductCategory.Enabled = false;
                cbbProducts.Enabled = false;
                txtQuantity.Enabled = false;
                btnAddtoCart.Enabled = false;
                dgCart.Enabled = false;


                int orderID = int.Parse(dgOrder.Rows[e.RowIndex].Cells["Order_ID"].Value.ToString());
                txtOrderID.Text = orderID.ToString();
                //This method groups Product information depending on order ID
                LoadProductGridViewDataByOrder(orderID);
                totalPrice = 0;
                lblTotalPrice.Text = "Total Price: " + dgOrder.Rows[e.RowIndex].Cells["TotalPrice"].Value.ToString();

                cbbCustomerList.SelectedValue = dgOrder.Rows[e.RowIndex].Cells["Customer_ID"].Value.ToString();
                cbbEmployeeList.SelectedValue = dgOrder.Rows[e.RowIndex].Cells["Employee_ID"].Value.ToString();

                for (int x = 0; x < dgOrder.RowCount; x++)
                {
                    dgOrder.Rows[x].DefaultCellStyle.BackColor = Color.White;
                    dgOrder.Rows[x].DefaultCellStyle.ForeColor = Color.Black;
                }
                dgOrder.CurrentRow.DefaultCellStyle.BackColor = Color.FromArgb(140, 18, 116);
                dgOrder.CurrentRow.DefaultCellStyle.ForeColor = Color.White;
            }
            catch (Exception)
            {

            }
        }

        private void LoadProductGridViewDataByOrder(int orderID)
        {//As the meaningful name suggests, this method provides a DataTable with
         //product data grouped by order ID.
            Product product = new Product();
            DataTable dt = product.GetProductDataByOrderID(orderID);
            dgCart.DataSource = dt;
        }

        private void btnAddtoCart_Click(object sender, EventArgs e)
        {
            try
            {
                int quantity = int.Parse(txtQuantity.Text);
                int productID = (int)cbbProducts.SelectedValue;
                string productCategory = cbbProductCategory.Text;
                string productBrand = cbbProducts.Text;

                
                foreach (Product pro in productPriceDic)
                {
                    if (pro.ID == productID)
                    { //This checks if the quantity of products desired
                      //can be purchased.
                        if (quantity <= pro.Stock)
                        {   //This helps calculate the total price by multiplying 
                            //the product price with the quantity entered
                            totalPrice += pro.Price * quantity;

                            OrderDetail order = new OrderDetail(productID, productBrand, quantity);
                            orderDetailList.Add(order);

                            var source = new BindingSource();
                            source.DataSource = orderDetailList;
                            dgCart.DataSource = source;
                        }
                        else
                        {//This gives meaningful feedback to the user, as per Schneiderman's rules
                            MessageBox.Show("Notice", "Remaining amount of this product on stock is " + pro.Stock.ToString());
                        }
                    }
                }
                //This sets the price that the Total Price in the Order Tab will show
                //while adding products.
                lblTotalPrice.Text = "Total Price: " + totalPrice.ToString();
            }
            catch { }
        }

        private void cbbProductCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string category = cbbProductCategory.Text;

            Product pro = new Product();
            //Gets DataTable of products in the Products table based on product Category
            DataTable dt = pro.GetProductListByCategory(category);

            if (dt.Rows.Count > 0)
            {
                cbbProducts.DataSource = dt;
                cbbProducts.DisplayMember = "Product_brand";
                cbbProducts.ValueMember = "Product_ID";
                cbbProducts.SelectedIndex = 0;

                
                productPriceDic = new List<Product>();
                foreach (DataRow row in dt.Rows)
                {
                    int productID = 0;
                    double price = 0;
                    int stock = 0;
                    try
                    {
                        productID = int.Parse(row.ItemArray[0].ToString());
                        price = double.Parse(row.ItemArray[4].ToString());
                        stock = int.Parse(row.ItemArray[5].ToString());
                    }
                    catch { }

                    productPriceDic.Add(new Product(productID, price, stock));


                }
            }
        }

        #endregion Order

        private void tcSaleManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcSaleManager.SelectedTab == tabOrder)
                LoadOrderData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        //Employee part of the form 
        #region Employee

        private void LoadEmployeeData()
        {
            LoadEmployeeGridViewData();

            tlpEmployee.Enabled = false;
            btnUpdateEmployee.Enabled = false;
            btnDeleteEmployee.Enabled = false;
        }

        private void LoadEmployeeGridViewData()
        {
            EmployeeDAL cusDAL = new EmployeeDAL();
            DataTable dt = cusDAL.GetEmployeeData();
            if (dt.Rows.Count > 0)
            {
                dgEmployee.DataSource = dt;
                dgEmployee.Columns["EmployeeID"].Visible = true;
            }
            else
                dgEmployee.DataSource = null;
        }

        private void ClearEmployeeInputFields()
        {
            txtEmployeeFirstName.Text = "";
            txtEmployeeLastName.Text = "";
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {//This basically turns the button from "New" to "Add" when clicked.
          //This code has been applied to other buttons as well.
            if (btnAddEmployee.Text == "New")
            {
                tlpEmployee.Enabled = true;
                btnAddEmployee.Text = "Add";
                ClearEmployeeInputFields();
                btnUpdateEmployee.Enabled = false;
                btnDeleteEmployee.Enabled = false;
            }
            else
            {
                tlpEmployee.Enabled = false;
                btnAddEmployee.Text = "New";
                btnUpdateEmployee.Enabled = false;
                btnDeleteEmployee.Enabled = false;

                try
                {
                    Employee Employee = new Employee(loginID, txtEmployeeFirstName.Text, txtEmployeeLastName.Text, txtStreet.Text, txtPostcode.Text, txtCity.Text, role);
                    Employee.Insert();
                    //Clears the textboxes
                    ClearEmployeeInputFields();
                    //Updates the Grid View
                    LoadEmployeeGridViewData();
                }
                catch (Exception)
                {
                    MessageBox.Show("Add operation failed.");
                }
            }
        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            btnAddEmployee.Enabled = true;
            btnUpdateEmployee.Enabled = false;
            btnDeleteEmployee.Enabled = false;
            tlpEmployee.Enabled = false;

            try
            {
                Employee Employee = new Employee(int.Parse(txtEmployeeID.Text), loginID, txtEmployeeFirstName.Text, txtEmployeeLastName.Text, txtStreet.Text, txtPostcode.Text, txtCity.Text, role);
                Employee.Update();
                //Clears the textboxes
                ClearEmployeeInputFields();
                //Updates the Grid View
                LoadEmployeeGridViewData();
            }
            catch (Exception)
            {
                MessageBox.Show("Update operation failed.");
            }
        }

        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            ClearEmployeeInputFields();
            btnAddEmployee.Enabled = true;
            btnUpdateEmployee.Enabled = false;
            btnDeleteEmployee.Enabled = false;
            tlpEmployee.Enabled = false;
            try
            {
                Employee Employee = new Employee(int.Parse(txtEmployeeID.Text));
                Employee.Delete();
                //Clears the textboxes
                ClearEmployeeInputFields();
                //Updates the Grid View
                LoadEmployeeGridViewData();
            }
            catch (Exception)
            {
                MessageBox.Show("Delete operation failed.");
            }
        }

        private void btnCancelEmployee_Click(object sender, EventArgs e)
        {
            btnAddEmployee.Text = "New";
            btnAddEmployee.Enabled = true;
            ClearEmployeeInputFields();
            btnUpdateEmployee.Enabled = false;
            btnDeleteEmployee.Enabled = false;
            tlpEmployee.Enabled = false;
        }

        private void dgEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                btnAddEmployee.Text = "New";
                btnAddEmployee.Enabled = false;
                btnUpdateEmployee.Enabled = true;
                btnDeleteEmployee.Enabled = true;
                tlpEmployee.Enabled = true;

                txtEmployeeID.Text = dgEmployee.Rows[e.RowIndex].Cells["EmployeeID"].Value.ToString();
                txtEmployeeFirstName.Text = dgEmployee.Rows[e.RowIndex].Cells["First_Name"].Value.ToString();
                txtEmployeeLastName.Text = dgEmployee.Rows[e.RowIndex].Cells["Last_Name"].Value.ToString();
                txtStreet.Text = dgEmployee.Rows[e.RowIndex].Cells["Address_Street"].Value.ToString();
                txtPostcode.Text = dgEmployee.Rows[e.RowIndex].Cells["Address_Postcode"].Value.ToString();
                txtCity.Text = dgEmployee.Rows[e.RowIndex].Cells["Address_City"].Value.ToString();

                for (int x = 0; x < dgEmployee.RowCount; x++)
                {
                    dgEmployee.Rows[x].DefaultCellStyle.BackColor = Color.White;
                    dgEmployee.Rows[x].DefaultCellStyle.ForeColor = Color.Black;
                }
                dgEmployee.CurrentRow.DefaultCellStyle.BackColor = Color.FromArgb(140, 18, 116);
                dgEmployee.CurrentRow.DefaultCellStyle.ForeColor = Color.White;
            }
            catch (Exception)
            {

            }
        }

        #endregion Employee

        //Product part of the form 
        #region Product 

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void ClearProductInputFields()
        {
            txtProductBrand.Text = "";
            txtProductCategory.Text = "";
            txtProductID.Text = "";
            txtProductPrice.Text = "";
            txtProductStock.Text = "";
        }

        
        private void btnCheckInfo_Click(object sender, EventArgs e)
        {
            try
            {

                    if (btnCheckInfo.Text == "Check")
                    {

                        btnCheckInfo.Text = "Check Information";
                        btnCheckInfo.Enabled = true;

                    }
                    else
                    {
                        btnAddEmployee.Text = "New Product Check";
                        
                    }

                    var productID = Convert.ToInt32(txtProductID.Text);

                    Product pro = new Product();
                    DataTable dt = pro.GetProductListByProductID(productID);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txtProductCategory.Text = dt.Rows[0].Field<string>("product_category").ToString();
                            txtProductBrand.Text = dt.Rows[0].Field<string>("product_brand").ToString();
                            txtProductPrice.Text = dt.Rows[0].Field<double>("product_price").ToString();
                            txtProductStock.Text = dt.Rows[0].Field<int>("product_stock").ToString();
                        }
                        catch { }
                    }

                
            }
            catch (Exception)
            {
                MessageBox.Show("Input for Product ID required!", "Notice");
            }

            
        }

        #endregion Product


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcSaleManager.SelectedIndex = 0;
        }

        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcSaleManager.SelectedIndex = 1;
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcSaleManager.SelectedIndex = 2;
        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tcSaleManager.SelectedIndex = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

