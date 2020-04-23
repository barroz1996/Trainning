using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.BusinessLayer.UserPackage
{

     class UserController
     {
        private bool HasLogged;
        private Dictionary<string, User> Users;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UserController()
        {
            this.HasLogged = false;
            this.Users = new Dictionary<string, User>();
           
        }
        public void LoadData() //Loads all the data while starting the program.
        {
            DataAccessLayer.User us = new DataAccessLayer.User();
            List<DataAccessLayer.User> dalus = us.FromJson(); //Gets a list of all DataAcessLayer users from the json file.
            foreach(DataAccessLayer.User dal in dalus)
            {
                Users.Add(dal.Email, new User(dal.Email, dal.Password, dal.Nickname, dal.LoggedIn));    //Adds all the users to the users dictionary.
            }
            foreach (KeyValuePair<string, User> entry in Users) //Checks if any of the users is logged in.
            {
                if (entry.Value.GetLoggedIn())
                    HasLogged = true;
            }
        }
        public User GetUser(string email) //Gets a specific user from the dictionary.
        {
            if (this.Users.ContainsKey(email))
                return Users[email];
            else
            {
                log.Debug("Tried getting unregistered user " + email);
                throw new Exception("This email is not registered.");
                
            }
        }

        public void Register(string email, string password, string nickname) //Adds a new user to the dictionary and creates a new json file for it.
        {
            if (!string.IsNullOrWhiteSpace(nickname))
            {
                Console.WriteLine("vsvs");
                foreach(KeyValuePair <string,User> nUser in Users)
                {
                    if (nUser.Value.GetNickname().Equals(nickname))
                    {
                        log.Debug("Error: nickName allready used!");
                        throw new Exception("nickName allready used!");
                    }
                }
                Users.Add(email, new User(email, password, nickname));
                GetUser(email).Save();
                log.Debug("User " + email + " was created.");
            }
            else
            {
                log.Debug("Error: nickName cant be empty");
                throw new Exception("nickName cant be empty");
            }
        }
        public bool IsLogged(string email) //Checks if a specific user is logged in.
        {
                return GetUser(email).GetLoggedIn();
        }
        public void Login(string email , string password) //Tries logging in a user.
        {if(!IsLogged(email))
                if(HasLogged == false) //User can only log in if everybody else is logged out.
                {
                    GetUser(email).Login(password); //Throws exception if password doesn't match.
                    HasLogged = true;
                }
                else
                {
                    log.Debug("Error: User " + email + " tried logging in while another user was already logged in.");
                    throw new Exception("Someone is already logged in.");
                }
           else
            {
                log.Debug("User " + email + " already is logged in");
                throw new Exception("user is already logged in");
            }
            
        }
        public void Logout(string email) //Logs a user out.
        {
                GetUser(email).Logout();
                HasLogged = false;
        }
        public void EmailVerify(string email) //Makes sure that the input email is valid.
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{3,9})$";
            if (!Regex.IsMatch(email, pattern)) //Checks that the email fits in the pattern.
            {
                log.Debug("illegal email");
                throw new Exception("please enter valid email");
            }
            if(Users.ContainsKey(email)) //Checks if this email is unused by another user.
            {
                log.Debug("Tried registering with an existing email.");
                throw new Exception("email already in use.");
            }
        }

        public void PasswordVerify(string password) //akes sure the input password is valid.
        {
            if (string.IsNullOrWhiteSpace(password)) //checks if the password is null.
            {
                log.Debug("empty password");
                throw new Exception("Password should not be empty");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(password)) //checks if it contains a lowercase letter.
            {
                log.Debug("Register password without lower case letter");
                throw new Exception("Password should contain at least one lower case letter.");
            }
            else
            {
                if (!hasUpperChar.IsMatch(password)) //checks if it contains an uppercase letter.
                {
                    log.Debug("Register password without upper case letter");
                    throw new Exception("Password should contain at least one upper case letter.");
                }
                else
                {
                    if (password.Length<4||password.Length>20) //checks if it fits the required length.
                    {
                        log.Debug("Register password out of bounds.");
                        throw new Exception("Password should not be lesser than 4 or greater than 20 characters.");
                    }
                    else
                    {
                        if (!hasNumber.IsMatch(password)) //checks contains it has a number.
                        {
                            log.Debug("Register password without a number");
                            throw new Exception("Password should contain at least one numeric value.");
                        }
                    }
                }
            }
        }
    }
}