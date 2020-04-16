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
        public string ToJson()
        {
            var json = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            


        }

        public T FromJson<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json);
        }
        public void Save(string toSave)
        {
            Console.WriteLine("h");
            
        }
    }
}
