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
        var user = new User(email, password, nickname);
        this.user.Add(email, user);
    }
               public User GetUser(string email)//check if list or dic
    {
                return User.getEmail;
    }
                public bool IsLogged(string email)
                   
    {
                return User.getEmail.Equals(email);
    }
  
   

}