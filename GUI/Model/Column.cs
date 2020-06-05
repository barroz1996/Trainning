using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Presentation.Model
{
    public struct Column
    {
        public readonly IReadOnlyCollection<Task> Tasks;
        public readonly string Name;
        public readonly int Limit;
    }
}
