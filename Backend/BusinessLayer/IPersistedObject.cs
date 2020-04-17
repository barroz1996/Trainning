using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    interface IPersistedObject<T> where T: DataAccessLayer.DalObject<T>
    {
        T ToDalObject();
    }
}
