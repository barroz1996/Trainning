using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;


namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{

    public class UserController
    {
        private bool HasLogged;
        //need to add constractor for creating new users with json
        private Dictionary<string, User> Users;
        public UserController()
        {
            this.HasLogged = false;
            this.Users = new Dictionary<string, User>();
        }
        

        public User GetUser(string email)
        {
            return Users[email];
        }

        public void Register(string email, string password, string nickname)
        {
            var user = new User(email, password, nickname);
            Users.Add(email, user);

        }
        public bool IsLogged(string email)
        {
            if (Users.ContainsKey(email))
                return Users[email].GetLoggedIn();
            return false;
        }
        public void Login(string email , string password)
        {
            if(HasLogged == false)
            {
                Users[email].Login(password);
                HasLogged = true;
            }
            else
            {
                throw new Exception("someone is already logged in");
            }
        }
        public void logout(string email)
        {
            if (Users[email].GetLoggedIn())
            {
                Users[email].Logout();
                HasLogged = false;
            }
            else
            {
                throw new Exception("user is not logged in");
            }
        }


    }

}