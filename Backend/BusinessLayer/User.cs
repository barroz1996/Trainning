using System;

public class User
{
    private string email;
    private string nickname;
    private string password;

    private bool LoggedIn;
    var serPass = new Service();

    public string GetEmail()
    {
        return this.email;
    }
    public string GetNickname()
    {
        return this.nickname;
    }
    public string GetPassword()
    {
        return this.password;
    }
    public void SetNickname(string nickname)
    {
        this.nickname = nickname;
    }

    public void Login(string password)
    {
        if (this.password.Equals(password))  //verify if the password match
        {
            Console.WriteLine("login was successfull");
            this.LoggedIn = true;
        }
        else
            throw new Exception("incorrect login information , please enter again");
    }
    public bool GetLoggedIn()
    {
        return this.loggedIn;
    }
    public void Logout() //updated the user status
    {
        this.loggedIn = false;
    }
}
