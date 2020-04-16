using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Column
    {
        private int columnOrdinal;
        private string columnName;
        private int limit;
        private List<Task> tasks;
        public Column(int columnOrdinal, string columnName,int limit, List<Task> tasks)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = limit;
            this.tasks = tasks;

        }
    }
}
