using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace Presentation.Model
{
    public class Board : NotifiableModelObject
    {
        private Board(BackendController controller, ObservableCollection<Column> columnsNames) : base(controller)
        {
            Columns = columnsNames;
            Columns.CollectionChanged += HandleChange;
        }

        public Board(BackendController controller, User user,string filter) : base(controller)
        {
            this.userEmail = user.Email;
            Columns = new ObservableCollection<Column>(controller.GetAllColumnNames(user.Email).
                Select((c, i) => new Column(controller,i, controller.GetTasks(user.Email,i,filter), controller.GetColumn(user.Email,i).Name, controller.GetColumn(user.Email, i).Limit, user.Email)).ToList());
            Columns.CollectionChanged += HandleChange;
        }
        public Board(BackendController controller, string email,string filter) : base(controller)
        {
            this.userEmail = email;
            Columns = new ObservableCollection<Column>(controller.GetAllColumnNames(email).
                Select((c, i) => new Column(controller, i, controller.GetTasks(email, i,filter), controller.GetColumn(email, i).Name, controller.GetColumn(email, i).Limit, email)).ToList());
            Columns.CollectionChanged += HandleChange;
        }
        public string userEmail { get; set; }
        public ObservableCollection<Column> _columns;
        public ObservableCollection<Column> Columns
        {
            get => _columns;
            private set
            {
                _columns = value;
                RaisePropertyChanged("Columns");
            }
        }
        private void HandleChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (Column y in e.OldItems)
                {

                    Controller.RemoveColumn(y.UserEmail,y.ColumnOrdinal);
                }

            }
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (Column y in e.NewItems)
                {
                    string k = "" + y.ColumnOrdinal;
                    Controller.AddColumn(y.UserEmail,y.Name, k);
                }

            }
            if (e.Action == NotifyCollectionChangedAction.Move)
            {
                foreach (Column y in e.OldItems)
                {

                   // Controller.mo(y.UserEmail, y.Name, y.ColumnOrdinal);
                }

            }
        }
    }   
}
