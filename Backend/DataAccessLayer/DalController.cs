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
        private string BaseUser;
        private string BaseBoard;
        protected readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DalController()
        {
            BaseUser = Directory.GetCurrentDirectory() + @"\files\Users\";
            BaseBoard = Directory.GetCurrentDirectory() + @"\files\Board\";
            if(!(new DirectoryInfo(BaseUser).Exists))
            {
                Directory.CreateDirectory(BaseUser);
            }
            if (!(new DirectoryInfo(BaseBoard).Exists))
            {
                Directory.CreateDirectory(BaseBoard);
            }
        }
        public void WriteUser(string fileName,string content)
        {
            File.WriteAllText(BaseUser+fileName + ".json", content);
            log.Info("The new data saved");   
        }
        public void WriteBoard(string fileName, string content)
        {
            File.WriteAllText(BaseBoard + fileName + ".json", content);
            log.Info("The new data saved");
        }
         
        public List<string> ReadFromBoardFile()
        {
            string[] fileName = Directory.GetFiles(BaseBoard);
            List<string> jsons = new List<string>();
            foreach (string file in fileName)
            {
                jsons.Add(File.ReadAllText(file));
            }
            return jsons;
        }

        public List<string> ReadFromUserFile()
        {
            string[] fileName = Directory.GetFiles(BaseUser);
            List<string> jsons = new List<string>();
            foreach (string file in fileName)
            {
                jsons.Add(File.ReadAllText(file));
            }
            return jsons;
        }
    }
}
