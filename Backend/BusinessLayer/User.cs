using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{
     class User : IPersistedObject<DataAccessLayer.User>
    {

        private string email;
        private string nickname;
        private string password;
        private bool LoggedIn;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public User() { }
        public User(string email, string password, string nickname)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.LoggedIn = false;
        }
        public User(string email, string password, string nickname,bool LoggedIn) {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.LoggedIn = LoggedIn;
        }
        public string GetEmail(){ return this.email; }
        public string GetNickname() { return this.nickname; }
        public string GetPassword(){ return this.password; }
        public bool GetLoggedIn() { return this.LoggedIn; }
        public void SetNickname(string nickname)
        {
            if (nickname == null)
            {
                log.Debug("User " + this.email + " tried setting a null nickname.");
                throw new Exception("Nickname cannot be null.");
            }
            else {
                this.nickname = nickname;
                log.Debug("User " + this.email + " changed his nickname to "+this.nickname+".");
            }
            
        }

        public void Login(string password) //tries logging in the user.
        {
                if (this.password.Equals(password))  //verify if the password matches the user's.
                {    
                    this.LoggedIn = true;
                    Save();                     //Saves in the json file that the user is logged in.
                    log.Debug("User " + email + " has logged in.");
                }
                else
                {
                    log.Debug("User " + this.email + " tried logging in with an incorrect password.");
                    throw new Exception("email and password does not match.");
                }   
        }
        public void Logout() //updated the user status
        {
            if (GetLoggedIn()) //checks if the user is logged in, else throws an exception.
            {
                this.LoggedIn = false;
                Save();
                log.Debug("User " + email + " has logged out.");
            }
            else
            {
                log.Debug("Error: User " + email + " tried logging out while being logged out.");
                throw new Exception("This user is not logged in");
            }
        }
        
        public DataAccessLayer.User ToDalObject() //returns a DataAccessLayer version of the current user.
        {
            return new DataAccessLayer.User(this.email, this.password, this.nickname,this.LoggedIn);
        }
        public void Save() //saves changes.
        {
            ToDalObject().Save();
        }
       
    }
}