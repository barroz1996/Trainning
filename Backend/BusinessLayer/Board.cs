using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
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


        public List<Column> GetColumns() { return columns; }
        public Column GetColumn(int columnOrdinal) // we get the key of the column and we return the column with this key
        {
            foreach (Column col in columns)
            {
                if (col.GetColumnOrdinal() == columnOrdinal)  // we check the columnOrdinal
                    return col;
            }
            throw new Exception("There mistake in colukmnOrdinal");
        }

        public Column GetColumn(string columnName) // we get the name of the column and we return the column with this name
        {
            foreach (Column col in columns)
            {
                if (col.GetColumnName().Equals(columnName))  // we check the columnName
                    return col;
            }
            throw new Exception("There mistake in the columnName");
        }
    }
}
