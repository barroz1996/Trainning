using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend
{
    class Main
    {
        public static void main(string[] args)
        {
            var service = new ServiceLayer.Service();
            service.LoadData();
            service.Register("asdf@gmail.com", "Aa123456", "asdf");
        }
    }
}
