using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;




public class UserController
{
        //need to add constractor for creating new users with json
    public string email;
    Dictionary<string, User> Users = new Dictionary<string, User>();
    
        public User GetUser(string email)
    {
        return Users[email];
    }

    public void Register(string email, string password, string nickname)
    {
        var user = new Users(email, password, nickname);
        Users.Add(email,user);

    }
    public bool IsLogged(string email)

    {
        User user;
        if (Users.ContainsKey(email))
             return Users.TryGetValue(email,user).IsLogged();
        return false;


    }



}

