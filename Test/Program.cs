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
            service.Register("yakov@gmail.com", "Kk4fs", "dark");
            service.Login(email, password);
            service.AddTask(email, "1vdssdv", "amos", DateTime.MaxValue);
            service.AddTask(email, "2vdssdv", "gershon", DateTime.MaxValue);
            service.AddTask(email, "3vdssdv", "mooo", DateTime.MaxValue);
            service.AddTask(email, "4vdssdv", "foo", DateTime.MaxValue);
            service.AddTask(email, "5vdssdaa", "roo", DateTime.MaxValue);
            service.LimitColumnTasks(email, 2, -3);
            service.LimitColumnTasks(email, 2, 5);
            service.LimitColumnTasks(email, 2, 2);
            service.LimitColumnTasks(email, 2, 0);
            service.AddColumn(email, 1, "new1");
            service.AdvanceTask(email, 0, 2);
            service.MoveColumnLeft(email, 3);
            service.AddColumn(email, 3, "new2");
            service.AdvanceTask(email, 1, 2);
            service.MoveColumnLeft(email, 3);
            service.AddColumn(email, 1, "new3");
            service.AdvanceTask(email, 2, 2);
            service.MoveColumnLeft(email, 3);
            service.AdvanceTask(email, 2, 3);
            Console.ReadKey();
        }
    }
}
