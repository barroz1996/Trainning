using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Board
    {
        private string email;
        private List<Column> columns;
        public Board(string email)
        {
            this.email = email;
            Column backlog = new Column(0, "BackLog");
            Column in_progress = new Column(1, "In Progress");
            Column done = new Column(2, "Done");
            columns.Add(backlog);
            columns.Add(in_progress);
            columns.Add(done);
        }
    }
}
