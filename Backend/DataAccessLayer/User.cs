using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class User:DalObject
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
    }
}
