using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class User : NotifiableModelObject
    {
        public User(BackendController controller, string email,string nickName) : base(controller)
        {
            this.Email = email;
            this.NickName = nickName;
        }
        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                RaisePropertyChanged("Email");
            }
        }

        private string _nickName;
        public string NickName
        {
            get => _nickName;
            set
            {
                _nickName = value;
                RaisePropertyChanged("NickName");
            }
        }


    }
}
