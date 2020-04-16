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
        public Board(string email, List<Column> columns)
        {
            this.email = email;
            this.columns = columns;
        }
    }
}
