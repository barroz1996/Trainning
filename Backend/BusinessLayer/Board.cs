using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Board
    {
        private string email;
        private List<Column> columns;
        public Board(string email)
        {
            this.email = email;
            this.totalTasks = 0;
            CreatColumns();

        }
        private void CreatColumns()// Private function that we(the programmers) build the columns.
        {
            Column backlog = new Column(0, "BackLog");
            Column in_prograss = new Column(1, "In_Prograss");
            Column done = new Column(2, "Done");
            columns.Add(backlog);
            columns.Add(in_prograss);
            columns.Add(done);
        }
        public int TotalNumberOfTasks()
        {
            return GetColumn(0).GetTasks().Count + GetColumn(1).GetTasks().Count + GetColumn(2).GetTasks().Count;
        }
        public List<Column> GetColumns() { return columns; }
        public Column GetColumn(int columnOrdinal) // we get the key of the column and we return the column with this key
        {
            foreach(Column col in columns)
            {
                if (col.GetColumnOrdinal() == columnOrdinal)  // we check the columnOrdinal
                    return col;
            }
            throw new Exception ("There mistake in colukmnOrdinal");
        }

        public Column GetColumn(string columnName) // we get the name of the column and we return the column with this name
        {
            foreach (Column col in columns)
            {
                if (col.GetColumnName().Equals(columnName))  // we check the columnName
                    return col;
            }
            throw new Exception ("There mistake in the columnName");
        }

        public void AdvanceTask(int columnOrdinal,int taskId)
        {
            Column addColumn = GetColumn(columnOrdinal+1);
            Column currenColumn = GetColumn(columnOrdinal);
            if (addColumn.GetLimit()> addColumn.GetTasks().Count)
            {
                Task advTask = currenColumn.GetTask(taskId);
                addColumn.RemoveTask(advTask);
                addColumn.AddTask(advTask);
            }
            else
            {
                Console.WriteLine("There is not ROOM in " + addColumn.GetColumnName());
            }

        }
    
    }
}
