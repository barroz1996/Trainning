using System;
using System.Collections.Generic;
using System.IO;
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
            Console.WriteLine(Directory.GetCurrentDirectory());
            service.Register("asdf@gmail.com", "Aaewoigjwg6", "asdf");
            service.Register("asdf@gmail.com", "Aaewoigjwgoiwejgoiwejoiwdwoekmgwfefwmefowkkkk6", "asdf");
            service.Login("asdf@gmail.com", "Aa12345");
            service.AddTask("asdf@gmail.com", "test", "description", DateTime.MaxValue);
            Console.WriteLine(" ");
            Console.ReadKey();
        }
    }
}
