using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleManager
{
    public partial class Login_Dialog : Form
    {
        string password = "";        
        public Login_Dialog()
        {
            InitializeComponent();
            cbbRole.SelectedIndex = 0;         
        }        

        private void btnLogin_Click(object sender, EventArgs e)
        {          

            User user = new User(txtUserName.Text, txtPassword.Text, cbbRole.Items[(int)cbbRole.SelectedIndex].ToString());
            if (!user.Exist())
            {
                MessageBox.Show("Incorrect Login credentials! Access denied.", "Notice");
                return;
            }
            else
            {
                Program.LoginID = user.GetLoginID();
                Program.Role = cbbRole.Items[(int)cbbRole.SelectedIndex].ToString();
                this.DialogResult = DialogResult.OK;
                this.Dispose();
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
