using System;

public class User
{
    private string email;
    private string nickName;
    private string password;
    private bool loggedIn;
    

    public string Email { get => email; set => email = value; }
    public string NickName { get => nickName; set => nickName = value; }
    public string Password { get => password; set => password = value; }

    public void Login(string password)
    {
        if (this.Password.Equals(password))//verify if the password match
        {
            Console.WriteLine("login was successfull");
            this.loggedIn = true;
        }
        else
            throw new Exception("incorrect login information , please enter again");
    }
    public bool getLoggedIn()
    {
        return this.loggedIn;
    }
    public void Logout() //updated the user status
    {
        this.loggenIn = false;
    }
}
