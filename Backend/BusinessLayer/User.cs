using System;

public class User
{
    private string email;
    private string nickName;
    private string password;

    public string Email { get => email; set => email = value; }
    public string NickName { get => nickName; set => nickName = value; }
    public string Password { get => password; set => password = value; }

    public void Login(string email, string password)
    {
        if (this.Password.Equals(password) && this.Email.Equals(email))//verify if the email and password match
        {
            Console.WriteLine("login was successfull");
            this.Login = true;
        }
        else
            throw new Exception("incorrect login information , please enter again");
    }
    public void Logout() //updated the user status
    {
        this.loggenIn = false;
    }
}
