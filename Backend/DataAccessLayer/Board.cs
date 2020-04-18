using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board : DalObject<Board>
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string email;
        private List<Column> columns;
        public Board() { }
        public Board(string email, List<Column> columns)
        {
            this.Email = email;
            this.Columns = columns;
        }
        public string Email { get => email; set => email = value; }
        public List<Column> Columns { get => columns; set => columns = value; }


        public string ToJson()
        {
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            log.Info("Board of user " + this.Email + " saved");
            return JsonSerializer.Serialize(this, json);
        }
        public void Save()
        {
            base.controller.WriteBoard(this.Email, ToJson());
        }
        public List<Board> FromJson()
        {
            List<Board> reBoard = new List<Board>();
            List<string> jsons = base.controller.ReadFromBoardFile();
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            foreach (string fromjs in jsons)
            {
                reBoard.Add(JsonSerializer.Deserialize<Board>(fromjs, json));
            }
            log.Info("All board data loaded");
            return reBoard;
        }
    }
}
