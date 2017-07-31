using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaleManager
{
    class User
    {
        string UserName, Password, Role;
        UserDAL userDAL = new UserDAL();

        public User() { }

        public User(string _userName, string _password, string _role)
        {
            UserName = _userName;
            Password = _password;
            Role = _role;
        }

        public bool Exist()
        {            
            return userDAL.IsValidUser(UserName, Password, Role);
        }

        public int GetLoginID()
        {
            return userDAL.GetLoginID(UserName, Password, Role);
        }

    }
}
