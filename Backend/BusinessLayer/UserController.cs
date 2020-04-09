using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;
public class UserController
{
    private ICollection <string , User>;

    public partial class Email : Form
    {
        public Email()
        {
            InitializeComponent;
        }
        public void Register(string email, string password, string nickName)
        {
            this.nickName = nickName;

        }
        private void EmailCheck(string email) //verify the email pattern
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(email, pattern))
                Console.WriteLine("success");
            else
                throw new Exception("please enter valid email");

        }
    }

}