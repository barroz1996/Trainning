using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using IntroSE.Kanban.Backend.ServiceLayer;
using System.Windows;
using Presentation.View;


namespace Presentation
{
    class MainWindowViewLogin : NotifiableObject
    {
        public BackendController Controller { get; private set; }
        public MainWindowViewLogin()
        {
            this.Controller=new BackendController();
            this.Email = "";
            this.Password = "";                               
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
        public string Password
        {
            get { return password; }
            set { password = value;
                RaisePropertyChanged("Password");
            }
        }
        public Model.User Login()
        {
            try
            {
                var res= Controller.Login(Email, Password);
                this.Email = "";
                this.Password = "";
                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
        }    
       public void Register()
        {
            RegisterWindow reg = new RegisterWindow(Controller);
            reg.ShowDialog();
        }
        


    }
}
