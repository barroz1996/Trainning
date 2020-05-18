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
            Service service = new Service();
            service.LoadData();
            service.Register("yakov@gmail.com", "Kk4fs", "dark");
            service.Login("yakov@gmail.com", "Kk4fs");
            service.AddTask("yakov@gmail.com", "amos", "gershon", DateTime.MaxValue);
            service.AdvanceTask("yakov@gmail.com", 0, 0);
            service.RemoveColumn("yakov@gmail.com", 1);
            Console.ReadKey();
        }
    }
}
