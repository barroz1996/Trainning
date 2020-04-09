using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
        public class UserController
{
               public void Register(string email , string password , string nickname)
    {
                   //code
    }
               public User GetUser(string email)
    {
                return User.getUser;
    }
                public bool IsLogged(string email)
                   
    {
                return User.getEmail.Equals(email);
    }
  
   

}