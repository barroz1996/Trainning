using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public struct Board
    {
        public readonly IReadOnlyCollection<string> ColumnsNames;
        public readonly string emailCreator;
    }
}
