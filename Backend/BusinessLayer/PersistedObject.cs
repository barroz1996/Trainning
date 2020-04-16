using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.BusinessLayer
{
    abstract class PersistedObject
    {
        private DataAccessLayer.DalObject toDalObject;
        protected PersistedObject()
        {
            this.toDalObject = new DataAccessLayer.DalObject();
        }
        protected void Save()
        {
            toDalObject.Save();
        }
        protected abstract T ToDalObject<T>();
        
    }

}
