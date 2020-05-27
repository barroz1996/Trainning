using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GUI
{
    class MainWindowViewLogin : INotifyPropertyChanged
    {
        private string email;
        public string Email
        {
            get { return Email; }
            set { Email = value; }
        }
        private string password;

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string property)
        {
            if(PropertyChanged!=null)
               PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        public string Password
        {
            get { return Password; }
            set { Password = value;
                RaisePropertyChanged("Password");
                Password = "bb";
            }
        }
        
    }
}
