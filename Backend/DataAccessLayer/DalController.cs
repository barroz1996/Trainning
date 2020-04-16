using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    class DalController
    {
        private string BasePath = @"C:\Users\Lenovo\milestones-2-asdf\files";
        protected readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public void WriteUser(string fileName,string content)
        {
            File.WriteAllText(fileName + ".txt,", content);
            log.Info("The new data saved");   
        }
        public void WriteBoard(string fileName, string content)
        {
            File.WriteAllText(+fileName + ".txt,", content);
            log.Info("The new data saved");
        }
        public 
        public string ReadFromFile(string fileName)
        {
               return File.ReadAllText(fileName);
        }
    }
}
