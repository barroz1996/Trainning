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
            if (this.Users.ContainsKey(email))
                return Users[email];
            else
                throw new Exception("This email is not registered.");
        }

        public void Register(string email, string password, string nickname)
        {
            var user = new User(email, password, nickname);
            Users.Add(email, user);
        }
        public bool IsLogged(string email)
        {
                return GetUser(email).GetLoggedIn();
        }
        public void Login(string email , string password)
        {
            if(HasLogged == false)
            {
                GetUser(email).Login(password);
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
                throw new Exception("User is not logged in");
            }
        }


    }

}