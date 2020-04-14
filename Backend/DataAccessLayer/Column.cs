using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Column:DalObject
    {
        private int columnOrdinal;
        private string columnName;
        private int limit;
        private List<Task> tasks;
        public Column(int columnOrdinal, string columnName)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = -1;
            this.tasks = new List<Task>();

        }
    }
}
