using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaleManager
{
    static class Program
    {
        
        public static int LoginID = 1;
        public static string Role = "Admin";

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            /*
             * For testing purposes, the commented line below can be uncommented
            and the lines from the instantiation of Login_Dialog to "break" can be commented out
            so as to disable the Login Form and just open the Form1
            */
            Application.Run(new Form1(LoginID, Role));

            //Login_Dialog newlogin = new Login_Dialog();
            //DialogResult result = newlogin.ShowDialog();
            //switch (result)
            //{
            //    case DialogResult.OK:
            //        Application.Run(new Form1(LoginID, Role));
            //        break;
            //    case DialogResult.Cancel:
            //        Application.Exit();
            //        break;
            //}
        }
    }
}
