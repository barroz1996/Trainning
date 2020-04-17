using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DalObject<T>
    {
        protected DalController controller;
        public DalObject()
            {
            this.controller=new DalController();
            }
   }
}
