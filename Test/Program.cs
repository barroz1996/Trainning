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
            var service = new Service();
            service.LoadData();

            //service.Register("fdfa@gmail.com", "hR3rth", "sadfSs");
            // service.Login("fdfa@gmail.com", "hR3rth");

            //service.Login("Sfsfa@gmail.com", "dasffF2da");
           // service.AddTask("Sfsfa@gmail.com", "fasf", "avdv", DateTime.MaxValue);
            service.GetColumn("Sfsfa@gmail.com", 0);

            
            
            Console.ReadKey();
        }
    }
}
