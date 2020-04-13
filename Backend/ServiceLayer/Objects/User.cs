using System;
using System.Text.RegularExpressions;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct User
    {
        public readonly string Email;
        public readonly string Nickname;
        internal User(string email, string nickname)
        {
            this.Email = email;
            this.Nickname = nickname;
        }
        public bool EmailVerify(string email)
        {
            string pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (Regex.IsMatch(email, pattern))
                return true;
            else
                throw new Exception("please enter valid email");

        }
        public bool PasswordVerify(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password should not be empty");
            }
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{4,20}");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(password))
            {
                Console.WriteLine("Password should contain at least one lower case letter.");
                return false;
            }
            else if (!hasUpperChar.IsMatch(password))
            {
                Console.WriteLine("Password should contain at least one upper case letter.");
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(password))
            {
                Console.WriteLine("Password should not be lesser than 4 or greater than 20 characters.");
                return false;
            }
            else if (!hasNumber.IsMatch(password))
            {
                Console.WriteLine("Password should contain at least one numeric value.");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
