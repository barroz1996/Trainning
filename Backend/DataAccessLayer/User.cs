using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class User : DalObject<User>
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string email;
        private string nickname;
        private string password;
        private bool loggedIn;

        public User() { }

        public User(string email, string password, string nickname, bool LoggedIn)
        {
            this.Email = email;
            this.Password = password;
            this.Nickname = nickname;
            this.LoggedIn = LoggedIn;
        }

        public string Email { get => email; set => email = value; }
        public string Nickname { get => nickname; set => nickname = value; }
        public string Password { get => password; set => password = value; }
        public bool LoggedIn { get => loggedIn; set => loggedIn = value; }

        public string ToJson()
        {
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            log.Debug("User " + this.Email + " saved");
            return JsonSerializer.Serialize(this, json);
        }
        public void Save()
        {
            base.controller.WriteUser(this.Email, ToJson());
        }
        public List<User> FromJson()
        {
            var reUser = new List<User>();
            var jsons = base.controller.ReadFromUserFile();
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            foreach (var fromjson in jsons)
            {
                reUser.Add(JsonSerializer.Deserialize<User>(fromjson, json));
            }
            log.Debug("All User data loaded");
            return reUser;
        }
    }
}
