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
        private bool LoggedIn;

        public User(string email, string password, string nickname, bool LoggedIn)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.LoggedIn = LoggedIn;
        }

        public string ToJson()
        {
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            log.Info("User" + this.email + " saved");
            return JsonSerializer.Serialize(this, json);
        }
        public void Save()
        {
            base.controller.WriteUser(this.email, ToJson());
        }
        public List<User> FromJson()
        {
            List<User> reUser = new List<User>();
            List<string> jsons = base.controller.ReadFromBoardFile();
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            foreach (string fromjs in jsons)
            {
                reUser.Add(JsonSerializer.Deserialize<User>(fromjs, json));
            }
            log.Info("All User data loaded");
            return reUser;
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

        public bool GetLoggedIn()
        {
            return this.LoggedIn;
        }
    }
}
