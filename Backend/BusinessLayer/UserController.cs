using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;


namespace IntroSE.Kanban.Backend.BusinessLayer
{

    public class UserController
    {
        //need to add constractor for creating new users with json
        private string email;
        Dictionary<string, User> Users = new Dictionary<string, User>();

        public User GetUser(string email)
        {
            return Users[email];
        }

        public void Register(string email, string password, string nickname)
        {
            var user = new Users(email, password, nickname);
            Users.Add(email, user);

        }
        public bool IsLogged(string email)

        {

            if (Users.ContainsKey(email))
                return Users[email].IsLogged();
            return false;
        }



    }

}