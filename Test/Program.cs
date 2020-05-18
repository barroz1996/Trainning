using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            string email = "yakov@gmail.com";
            string password = "Kk4fs";
            Service service = new Service();
            service.LoadData();
            //service.Register("yakov@gmail.com", "Kk4fs", "dark");
            //service.Login(email, password);
            //service.AddTask(email, "title", "", DateTime.MaxValue);
            service.AddColumn(email, 1, "newCol");
            //service.AdvanceTask(email, 0, 0);
            Console.ReadKey();
        }
    }
}
