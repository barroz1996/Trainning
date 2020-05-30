using IntroSE.Kanban.Backend.BusinessLayer;
namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public struct User
    {
        public readonly string Email;
        public readonly string Nickname;
        internal User(string email, string nickname)
        {
            Email = email;
            Nickname = nickname;
        }
        /*
        public string GetLogUser()
        {
            var UserController = new BusinessLayer.UserPackage.UserController();
            UserController.LoadData();
            if (UserController.GetHasLogged())
            {
                foreach(var user in UserController.GetUsers())
                {
                    if (user.Value.GetLoggedIn())
                    {
                        return user.Value.GetEmail();
                    }
                }
            }
            return null;
        }
        */
    }
}
