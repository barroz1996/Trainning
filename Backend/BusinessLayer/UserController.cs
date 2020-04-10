using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public class UserController
{
    public string email;
    Dictionary<string, email > Users = new Dictionary<string, email>();

    public void Register(string email, string password, string nickname)
    {
        var user = new Users(email, password, nickname);
        Users.Add(email);

    }
    public bool IsLogged(string email)

    {
        if (email.Equals(Users.getEmail))
            return true;
        return false;

    }



}