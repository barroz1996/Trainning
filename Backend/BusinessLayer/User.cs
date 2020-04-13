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
            this.nickname = nickname;
        }

        public void Login(string password)
        {
            if (this.password.Equals(password))  //verify if the password match
            {
                Console.WriteLine("login was successfull");
                this.LoggedIn = true;
            }
            else
                throw new Exception("incorrect login information , please enter again");
        }
        public bool GetLoggedIn()
        {
            return this.LoggedIn;
        }
        public void Logout() //updated the user status
        {
            this.LoggedIn = false;
        }
    }
}