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
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Column() { }
        public Column(int columnOrdinal, string columnName)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = -1;
            this.tasks = new List<Task>();

        }
        public Column(int columnOrdinal, string columnName, int limit)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = limit;
            this.tasks = new List<Task>();
        }

        public int GetColumnOrdinal() { return this.columnOrdinal; }
        public string GetColumnName() { return this.columnName; }
        public int GetLimit() { return this.limit; }
        public void LimitColumnTasks(int limit) //Sets a limit to the current column.
        {
            if (limit < -1)
            {
                log.Debug("Tried setting an illegal limit");
                throw new Exception("Illegal limit.");  //legal limit values are >=-1.
            }
            if ((limit < tasks.Count) && (limit > -1))
            {
                log.Debug("Tried setting a limit lower than the current number of tasks to the current column.");
                throw new Exception("This column currently holds more tasks than the limit.");// if in the column right now more tasks then the new limit
            }
            this.limit = limit;
        }
        public List<Task> GetTasks() { return this.tasks; } //Gets the list of tasks in the current column.


        public void AddTask(Task newTask)   //Adds task to the current column.
        {
            if (tasks.Count < limit || limit == -1) //Checks if there's room for another task in the current column.
                tasks.Add(newTask);
            else
            {
                log.Debug("Tried adding a task to a full column.");
                throw new Exception("The " + this.columnName + " column is aleady full.");
            }
        }

        public Task RemoveTask(int taskId) //Removes task from the current column.
        {
            var getTask = GetTask(taskId);
            tasks.Remove(getTask);
            return getTask;

        }
        public Task GetTask(int taskId) //Task getter, throws exception if not found.
        {
            foreach (var task in tasks)
            {
                if (task.GetTaskID() == taskId)
                    return task;
            }
            log.Debug("Task " + taskId + " does not exist in the current column.");
            throw new Exception("The task doesn't exist in this column.");
        }
        public void SetTasks(List<Task> task)
        {
            foreach (var newTask in task)
            {
                AddTask(newTask);
            }
        }
        public void SetOrdinal(int columnOrdinal)
        {
            this.columnOrdinal = columnOrdinal;
        }

    }
}

