using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    class Column
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

        public int GetColumnOrdinal() { return this.columnOrdinal; }
        public string GetColumnName() { return this.columnName; }
        public void SetColumnName(string columnName) { this.columnName = columnName; }
        public int GetLimit() { return this.limit; }
        public void LimitColumnTasks(int limit)
        {
            if (limit < -1)
                throw new Exception("Illegal limit.");  // it cant be under 1
            if ((limit < tasks.Count)&&(limit>-1))
                throw new Exception("This column currently holds more tasks than the limit.");      // if in the column right now more tasks then the new limit
            this.limit = limit;
        }
        public List<Task> GetTasks() { return this.tasks; }


        public void AddTask(Task newTask)
        {
            if (tasks.Count < limit || limit == -1)  // we check if there to much tasks
                tasks.Add(newTask);
            else
                throw new Exception("The " + this.columnName + " column is aleady full.");
        }

        public Task RemoveTask(int taskId)
        {
            Task getTask = GetTask(taskId);
            tasks.Remove(getTask); // return true if the occurance delete, exeception if not found
            return getTask;
           
        }

        public Task GetTask(int taskId)
        {
            foreach (Task task in tasks)
            {
                if (task.GetTaskID() == taskId)
                    return task;     // we return if the task removed from the column
            }
            throw new Exception("The task doesn't exist in this column."); // if the task not in the column

        }
    }
}
