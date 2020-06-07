using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using IntroSE.Kanban.Backend.ServiceLayer;

namespace Presentation.Model
{
    public struct Column
    {
        public readonly IReadOnlyCollection<Task> Tasks;
        public readonly string Name;
        public readonly int Limit;
        public Column(IntroSE.Kanban.Backend.ServiceLayer.Column col)
        {
            List<Model.Task> temp = new List<Model.Task>();
            foreach(var task in col.Tasks)
            {
                temp.Add(new Task(task));
            }
            this.Tasks = (IReadOnlyCollection<Model.Task>)temp;
            this.Name = col.Name;
            this.Limit = col.Limit;
        }
    }
}
