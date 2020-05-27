using System;
using System.Collections.Generic;

namespace IntroSE.Kanban.Backend.BusinessLayer.BoardPackage
{
    internal class Column
    {
        private int columnOrdinal;
        private readonly string columnName;
        private int limit;
        private List<Task> tasks;
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Column() { }
        public Column(int columnOrdinal, string columnName)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            limit = 100;
            tasks = new List<Task>();

        }
        public Column(int columnOrdinal, string columnName, int limit)
        {
            this.columnOrdinal = columnOrdinal;
            this.columnName = columnName;
            this.limit = limit;
            tasks = new List<Task>();
        }

        public int GetColumnOrdinal() { return columnOrdinal; }
        public string GetColumnName() { return columnName; }
        public int GetLimit() { return limit; }
        public void LimitColumnTasks(int limit) //Sets a limit to the current column.
        {
            if (limit < -1)
            {
                log.Debug("Tried setting an illegal limit");
                throw new Exception("Illegal limit.");  //legal limit values are >=-1.
            }
            if ((limit < tasks.Count) && (limit > -1))
            { //the column's limit can't be lower than the current number of tasks the column holds.
                log.Debug("Tried setting a limit lower than the current number of tasks to the current column.");
                throw new Exception("This column currently holds more tasks than the limit.");
            }
            this.limit = limit;
        }
        public List<Task> GetTasks() { return tasks; } //Gets the list of tasks in the current column.


        public void AddTask(Task newTask)   //Adds task to the current column.
        {
            if (tasks.Count < limit || limit == -1) //Checks if there's room for another task in the current column.
            {
                tasks.Add(newTask);
            }
            else
            {
                log.Debug("Tried adding a task to a full column.");
                throw new Exception("The " + columnName + " column is aleady full.");
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
                {
                    return task;
                }
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

