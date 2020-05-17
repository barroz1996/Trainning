using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class UserDTO
    {
        public const string UsersEmailColumn = "Email";
        public const string UsersNicknameColumn = "Nickname";
        public const string UsersPasswordColumn = "Password";
        public const string UsersLoggedInColumn = "LoggedIn";
        private Controllers.UserControl _controller;
        private string _email;
        private string _nickName;
        private string _password;
        private bool _loggedIn;
        public UserDTO(string email,string nickName,string password,bool loggedIn)
        {
            this._email = email;
            this._nickName = nickName;
            this._password = password;
            this._loggedIn = loggedIn;
            _controller = new Controllers.UserControl();
          
        }

        public string Email { get => _email; set => _email = value; }
        public string Nickname { get => _nickName; set { _nickName = value; } }
        public string Password { get => _password; set { _password = value; } }
        public bool LoggedIn { get => _loggedIn; set { _loggedIn = value; } }
    }
}
