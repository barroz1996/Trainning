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
        private string BaseLogs;
        public DalController()
        {
            BaseUser = Directory.GetCurrentDirectory() + @"\files\Users\";
            BaseBoard = Directory.GetCurrentDirectory() + @"\files\Board\";
            BaseLogs = Directory.GetCurrentDirectory() + @"\files\Logs\";
            if (!(new DirectoryInfo(BaseUser).Exists))
            {
                Directory.CreateDirectory(BaseUser);
            }
            if (!(new DirectoryInfo(BaseBoard).Exists))
            {
                Directory.CreateDirectory(BaseBoard);
            }
            if (!(new DirectoryInfo(BaseLogs).Exists))
            {
                Directory.CreateDirectory(BaseLogs);
            }
        }
        public void WriteUser(string fileName, string content)
        {
            File.WriteAllText(BaseUser + fileName + ".json", content);
        }
        public void WriteBoard(string fileName, string content)
        {
            File.WriteAllText(BaseBoard + fileName + ".json", content);
        }
        public void WriteLogs(string fileName, string content)
        {
            File.WriteAllText(BaseLogs + fileName + ".json", content);
        }

        public List<string> ReadFromBoardFile()
        {
            var fileName = Directory.GetFiles(BaseBoard);
            var jsons = new List<string>();
            foreach (var file in fileName)
            {
                jsons.Add(File.ReadAllText(file));
            }
            return jsons;
        }

        public List<string> ReadFromUserFile()
        {
            var fileName = Directory.GetFiles(BaseUser);
            var jsons = new List<string>();
            foreach (var file in fileName)
            {
                jsons.Add(File.ReadAllText(file));
            }
            return jsons;
        }
    }
}
