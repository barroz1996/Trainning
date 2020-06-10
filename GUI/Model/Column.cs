using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Presentation.Model 
{
    public class Column:NotifiableModelObject
    {


        public readonly string UserEmail;
        public Column(BackendController controller,int ordinal, List<IntroSE.Kanban.Backend.ServiceLayer.Task> tasks,string name,int limit,string userEmail) : base(controller)
        {       
            Tasks = new ObservableCollection<Task>(tasks.
               Select((c, i) => new Task(controller, ordinal, userEmail, tasks[i])).ToList());
            this.UserEmail = userEmail;
            this.Name = name;
            this.Limit = limit;
            this.ColumnOrdinal = ordinal;
            Tasks.CollectionChanged += HandleChange;
        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Task y in e.OldItems)
                {
                    Controller.DeleteTask(y);
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Task y in e.NewItems)
                {
                    Controller.AddTask(y.userEmail,y.Title,y.Description,y.DueDate);
                }
            }
        }
        private ObservableCollection<Task> _tasks;
        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                RaisePropertyChanged("Tasks");
            }
        }
        private Model.Task task;
        public Model.Task Task
        {
            get { return task; }
            set
            {
                task = value;
                RaisePropertyChanged("Task");
            }
        }
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        private int _limit;
        public int Limit
        {
            get => _limit;
            set
            {
                _limit = value;
                RaisePropertyChanged("Limit");
            }
        }
        private int _columnOrdinal;
        public int ColumnOrdinal
        {
            get => _columnOrdinal;
            set
            {
                _columnOrdinal = value;
                RaisePropertyChanged("ColumnOrdinal");
            }
        }
    }
}
