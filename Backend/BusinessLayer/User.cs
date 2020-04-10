using System;

public class User
{
    private string email;
    private string nickname;
    private string password;
    private bool loggedIn;
    var serPass = new Service();
    public string getEmail(string email)
    {
        this.email = email;
    }
    public string getNickname(string nickname)
    {
        this.nickname = nickname;
    }
    public string getPassword(string password)
    {
        this.password = password;
    }

    public void Login(string password)
    {
        //if (password.Equals(serPass.login)  //verify if the password match
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
        this.loggedIn = false;
    }
}
