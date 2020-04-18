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
        public Column() { }
        public Column(int columnOrdinal, string columnName, int limit, List<Task> tasks)
        {
            this.ColumnOrdinal = columnOrdinal;
            this.ColumnName = columnName;
            this.Limit = limit;
            this.Tasks = tasks;
        }

        public int ColumnOrdinal { get => columnOrdinal; set => columnOrdinal = value; }
        public string ColumnName { get => columnName; set => columnName = value; }
        public int Limit { get => limit; set => limit = value; }
        internal List<Task> Tasks { get => tasks; set => tasks = value; }
    }
}
