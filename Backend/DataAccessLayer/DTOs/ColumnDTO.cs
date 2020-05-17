using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    class ColumnDTO
    {
        public const string ColumnOrdinalColumnOrdinal = "ColumnOrdinal";
        public const string ColumnNameColumnName = "ColumnName";
        public const string ColumnLimitColumnLimit = "Limit";
        public const string ColumnEmailColumnEmail = "Email";
        private Controllers.ColumnControl _controller;
        private int _columnOrdinal;
        private string _columnName;
        private int _limit;
        private string _email;
        public ColumnDTO(int columnOrdinal,string columnName,int limit,string email)
        {
            this._columnOrdinal = columnOrdinal;
            this._columnName = columnName;
            this._limit = limit;
            this._email = email;
            _controller = new Controllers.ColumnControl();
            _controller.Insert(this);
        }

        public int ColumnOrdinal { get => _columnOrdinal; set { _columnOrdinal = value; _controller.Update(_columnOrdinal, ColumnOrdinalColumnOrindal, value); } }
        public string ColumnName { get => _columnName; set { _columnName = value; _controller.Update(_columnOrdinal, ColumnNameColumnName, value); } }
        public int Limit { get => _limit; set { _limit = value; _controller.Update(_columnOrdinal, ColumnLimitColumnLimit, value); } }
        public string Email { get => _email; set => _email = value; }
         public bool Delete()
        {
           return _controller.Delete(this);
        }
    }
}
