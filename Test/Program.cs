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
            service.Register("asdf@gmail.com", "Aa1234", "asdf");
            Console.ReadKey();
        }
    }
}
