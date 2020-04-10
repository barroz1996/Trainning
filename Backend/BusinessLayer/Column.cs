using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    class Column
    {   
        private int columnOrdinal;
        private string columnName;
        private int limit;
        private List<Task> tasks;
        public Column(int columnOrdinal , string columnName)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = Int32.MaxValue;
            this.tasks = new List<Task>();
             
        }
      
        public int GetColumnOrdinal() { return this.columnOrdinal; }
        public string GetColumnName() { return this.columnName; }
        public void SetColumnName(string columnName) { this.columnName = columnName; }
        public int GetLimit() { return this.limit; }
        public void LimitColumnTasks(int limit)
        {
            if (limit < 1)
                throw new Exception("limit not possible");  // it cant be under 1
            if(limit<tasks.Count)
                throw new Exception("to much tasks");      // if in the column right now more tasks then the new limit
            this.limit = limit;
        }
        public List<Task> GetTasks() { return this.tasks; }

        public void AddTask(Task newTask)
        {
            if (tasks.Count < limit)  // we check if there to much tasks
                tasks.Add(newTask);
            else
                throw new Exception("There is no room for new task");
        }

        public Boolean RemoveTask(int taskId)
        {  
            foreach(Task task in tasks)
            {
                if (task.GetTaskId() == taskId) 
                    return tasks.Remove(task);     // we return if the task removed from the column
            }
            throw new Exception("the task not found"); // if the task not in the column
        }
        
    }
}
