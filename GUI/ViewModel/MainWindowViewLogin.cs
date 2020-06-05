using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;


namespace Presentation
{
    class MainWindowViewLogin : INotifyPropertyChanged
    {
        Service service;
        public MainWindowViewLogin()
        {
            this.email = "";
            this.password = "";
            this.regEmail = "";
            this.regPassword = "";
            this.nickName = "";
            this.host = "";
            service = new Service();
            service.LoadData();
           
        }
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value;
                RaisePropertyChanged("Email");
            }
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
            get { return password; }
            set { password = value;
                RaisePropertyChanged("Password");
            }
        }
        public void Login(string email,string password)
        {
            Response<User> logUser = service.Login(email, password);
            if (logUser.ErrorOccured)
            {
                MessageBox.Show(logUser.ErrorMessage);
               
            }
            else
            {
                
                Email = "";
                Password = "";
                RegEmail = "";
                RegPassword = "";
                NickName = "";
                Host = "";
                var Board = new BoardWindow(service, email);
                Board.ShowDialog();
                         
            }
        }
      
        private string regEmail;
        public string RegEmail
        {
            get { return regEmail; }
            set
            {
                regEmail = value;
                RaisePropertyChanged("RegEmail");
            }
        }
        private string regPassword ;
        public string RegPassword
        {
            get { return regPassword; }
            set
            {
                regPassword = value;
                RaisePropertyChanged("RegPassword");
            }
        }
        private string nickName ;
        public string NickName
        {
            get { return nickName; }
            set
            {
                nickName = value;
                RaisePropertyChanged("NickName");
            }
        }
        private string host ;
        public string Host
        {
            get { return host; }
            set
            {
                host = value;
                RaisePropertyChanged("Host");
            }
        }
        public void Register(string email,string password,string nickName,string host)
        {
            Response reg;
            if (string.IsNullOrWhiteSpace(host))
            {
               reg= service.Register(email, password, nickName);
            }
            else
            {
                reg = service.Register(email, password, nickName,host);
            }
            if (reg.ErrorOccured)
            {
                MessageBox.Show(reg.ErrorMessage);
            }
            else
            {
                Email = "";
                Password = "";
                RegEmail = "";
                RegPassword = "";
                NickName = "";
                Host = "";               
                MessageBox.Show(email+" registered successfully!");
            }
        }
        


    }
}
