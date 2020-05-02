using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Board : IPersistedObject<DataAccessLayer.Board>
    {
        //fields
        private string email;
        private List<Column> columns;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Board(string email) //ctor
        {
            this.email = email;
            Column backlog = new Column(0, "backlog");
            Column in_progress = new Column(1, "in progress");
            Column done = new Column(2, "done");
            this.columns = new List<Column>();
            columns.Add(backlog);
            columns.Add(in_progress);
            columns.Add(done);
        }
        public Board() { }
        public Board(string email, List<Column> columns)
        {
            this.email = email;
            this.columns = columns;
        }
        public String GetEmail() { return email; }
        public List<Column> GetColumns() { return columns; }
        public Column GetColumn(int columnOrdinal) // we get the key of the column and we return the column with this key
        {
            foreach (var column in columns)
            {
                if (column.GetColumnOrdinal() == columnOrdinal)  // we check the columnOrdinal
                    return column;
            }
            log.Debug("Tried getting an illegal column ordinal.");
            throw new Exception("Column ordinal is illegal.");
        }

        public Column GetColumn(string columnName) // we get the name of the column and we return the column with this name
        {
            foreach (var column in columns)
            {
                if (column.GetColumnName().Equals(columnName))  // we check the columnName
                    return column;
            }
            log.Debug("Tried getting an illegal column name.");
            throw new Exception("Column Name is illegal");
        }
        public void AddTask(int taskId, String title, String description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                log.Debug("Tried adding new task with an empty title");
                throw new Exception("Title can't be empty.");
            }
            if (title.Length > 50)
            {
                log.Debug("Tried adding new task with an title longer than 50 characters.");
                throw new Exception("Title can't be longer than 50 characters.");
            }
            if (description != null)
            {
                if (description.Length > 300)
                {
                    log.Debug("Tried adding new task with an description longer than 300 characters.");
                    throw new Exception("Description can't be longer than 300 characters.");
                }
            }
            if (!(DateTime.Compare(dueDate, DateTime.Now) > 0))
            {
                log.Debug("Tried adding new task with a non futuristic due date.");
                throw new Exception("Due date is required to be a futuristic date.");
            }
            GetColumn(0).AddTask(new Task(taskId, title, description, dueDate));
            Save();
            log.Debug("Task " + (taskId) + " was created by user " + email + ".");
        }

        public DataAccessLayer.Board ToDalObject()
        {
            var column = new List<DataAccessLayer.Column>();
            foreach (var col in this.columns)
            {
                column.Add(col.ToDalObject());
            }
            return new DataAccessLayer.Board(this.email, column);
        }
        public void Save()
        {
            ToDalObject().Save();
        }
        public int TotalTask()
        {
            return GetColumn(0).GetTasks().Count + GetColumn(1).GetTasks().Count + GetColumn(2).GetTasks().Count;
        }
        public Column AddColumn(int columnOrdinal, string Name)
        {
            if (string.IsNullOrWhiteSpace(Name) || Name.Length > 15)
                throw new Exception("Name out of bounds!");
            foreach(Column col in columns)
            {
                if (col.GetColumnName().Equals(Name))
                    throw new Exception("This column name already exists");
            }
            if(columnOrdinal!=columns.Count)
               checkOrdinal(columnOrdinal);
            Column newCol = new Column(columnOrdinal, Name);
            columns.Insert(columnOrdinal, newCol);
            for(int i = columnOrdinal + 1; i < columns.Count; i = i + 1) // we update the columnOrdinal we moved
            {
                GetColumn(i).SetOrdinal(i);
            }
            return newCol;
        }
        public Column MoveColumn(int columnOrdinal,int direction)
        {
            checkOrdinal(columnOrdinal);
            if (direction == 1 && columnOrdinal == columns.Count - 1)
                throw new Exception("Last column can not be moved right!");
            if (direction == -1 && columnOrdinal == 0)
                throw new Exception("First column can not be moved Left!");
           return SwapCol(columnOrdinal, columnOrdinal + direction);
            
        }
        private Column SwapCol(int columnOrdinal1,int columnOrdinal2)
        {
            Column sCol = GetColumn(columnOrdinal1);
            columns[columnOrdinal1] = GetColumn(columnOrdinal2);
            columns[columnOrdinal2] = sCol;
            GetColumn(columnOrdinal1).SetOrdinal(columnOrdinal2);
            GetColumn(columnOrdinal2).SetOrdinal(columnOrdinal1);
            return GetColumn(columnOrdinal2);
        }
        private void checkOrdinal(int columnOrdinal)
        {
            if ((columns.Count < columnOrdinal) || (columnOrdinal < 0))
                throw new Exception("The columnOrdinal out of bounds!");
        }
        public void RemoveColumn(int columnOrdinal)
        {
            if (columns.Count == 2)
                throw new Exception("Cannot have less than 2 columns!");
            checkOrdinal(columnOrdinal);
            if (columnOrdinal == 0)
            {
                if (GetColumn(1).GetLimit() < GetColumn(1).GetTasks().Count + GetColumn(0).GetTasks().Count)
                    throw new Exception("The next column cannot hold all the tasks!");
                GetColumn(1).GetTasks().AddRange(GetColumn(columnOrdinal).GetTasks());
            }
            else
            {
                if (GetColumn(columnOrdinal - 1).GetLimit() < GetColumn(columnOrdinal).GetTasks().Count + GetColumn(columnOrdinal - 1).GetTasks().Count)
                    throw new Exception("The previous column cannot hold all the tasks!");
                GetColumn(columnOrdinal - 1).GetTasks().AddRange(GetColumn(columnOrdinal).GetTasks());
            }
            columns.Remove(GetColumn(columnOrdinal));
            for (int i = columnOrdinal+1; i < columns.Count; i = i + 1) // we update the columnOrdinal we moved
            {
                GetColumn(i).SetOrdinal(i-1);
            }

        }
    }
}
