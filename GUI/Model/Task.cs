using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    public struct Task
    {
        public readonly int TaskId;
        public string Title;
        public string Description;
        public readonly DateTime CreationDate;
        public DateTime DueDate;
        public string EmailAssignee;
    }
}
