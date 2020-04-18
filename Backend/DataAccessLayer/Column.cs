using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class Column :DalObject<Column>
    {
        private int columnOrdinal;
        private string columnName;
        private int limit;
        private List<Task> tasks;
        public Column(int columnOrdinal, string columnName, int limit, List<Task> tasks)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = limit;
            this.tasks = tasks;

        }

        public int GetColumnOrdinal() { return this.columnOrdinal; }
        public string GetColumnName() { return this.columnName; }
        public int GetLimit() { return this.limit; }
        public List<Task> GetTasks() { return this.tasks; }
    }
}
