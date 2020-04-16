using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
    public class User
    {

        private string email;
        private string nickname;
        private string password;
        private bool LoggedIn;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.LoggedIn = false;
        }
        public string GetEmail()
        {
            return this.email;
        }
        public string GetNickname()
        {
            return this.nickname;
        }
        public string GetPassword()
        {
            return this.password;
        }
        public void SetNickname(string nickname)
        {
            if (nickname == null)
            {
                log.Info("User " + this.email + " tried setting a null nickname.");
                throw new Exception("Nickname cannot be null.");
            }
            else {
                this.nickname = nickname;
                log.Info("User " + this.email + " changed his nickname to "+this.nickname+".");
            }
            
        }

        public void Login(string password)
        {
            if (this.password.Equals(password))  //verify if the password match
            {
                this.LoggedIn = true;
            }
            else
            {
                log.Info("User " + this.email + " tried logging in with an incorrect password.");
                throw new Exception("email and password does not match.");
            }
                
        }
        public bool GetLoggedIn()
        {
            return this.LoggedIn;
        }
        public void Logout() //updated the user status
        {
            this.LoggedIn = false;
        }
        protected abstract void Save()
        {

        }
        protected T ToDalObject<T>()
        {
            return new DataAccessLayer.DalObject<User>//(email, password, nickname, LoggedIn);
        }



    }
}