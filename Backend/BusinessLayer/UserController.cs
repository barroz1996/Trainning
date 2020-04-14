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
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            {
                log.Info("Tried getting unregistered user " + email);
                throw new Exception("This email is not registered.");
            }
        }

        public void Register(string email, string password, string nickname)
        {
            var user = new User(email, password, nickname);
            Users.Add(email, user);
            log.Info("User "+email+" was created.");
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
                log.Info("User " + email + " has logged in.");
            }
            else
            {
                log.Info("Error: User " + email + " tried logging in while another user was already logged in.");
                throw new Exception("Someone is already logged in.");
            }
        }
        public void Logout(string email)
        {
            if (GetUser(email).GetLoggedIn())
            {
                GetUser(email).Logout();
                HasLogged = false;
                log.Info("User " + email + " has logged out.");
            }
            else
            {
                log.Info("Error: User " + email + " tried logging out while being logged out.");
                throw new Exception("This user is not logged in");
            }
        }


    }

}