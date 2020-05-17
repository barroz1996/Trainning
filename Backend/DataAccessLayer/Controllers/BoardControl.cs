using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class BoardControl
    {

        private readonly string _connectionString;
        private readonly string _tableName;
        public BoardControl()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db3"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = "Boards";
        }
 
        public List<DTOs.BoardDTO> Select()
        {
            List<DTOs.BoardDTO> boardsList = new List<DTOs.BoardDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = $"select* from {_tableName}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        boardsList.Add(new DTOs.BoardDTO(dataReader.GetString(0)));
                    }
                    ColumnControl _colControl = new ColumnControl();
                    _colControl.
                }
                finally
                {
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                    command.Dispose();
                    connection.Close();
                }
               

            }
            return boardsList;
        }

        public bool Delete(DTOs.TaskDTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"delete from {_tableName} where id={DTOObj.TaskId}"
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool Insert(DTOs.TaskDTO Tasks)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} where ID={Tasks.TaskId}, ({DTOs.TaskDTO.TasksColumnIdColumnColumnId} ,{DTOs.TaskDTO.TasksTitleColumnTitle},{DTOs.TaskDTO.TasksDescriptionColumnDescription},{DTOs.TaskDTO.TasksDueDateColumnDueDate},{DTOs.TaskDTO.TasksCreationDateColumnCreationDate},{DTOs.TaskDTO.TasksEmailColumnEmail},{DTOs.TaskDTO.TasksIdColumnId}) " +
                        $"VALUES (@idVal,@titleVal,@descriptionVal,@dueDateTimeVal,@creationTimeVal,@emailVal,@columnOridnalVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", Tasks.TaskId);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", Tasks.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"descriptionVal", Tasks.Description);
                    SQLiteParameter dueDateParam = new SQLiteParameter(@"dueDateTimeVal", Tasks.DueDate);
                    SQLiteParameter creationTimeParam = new SQLiteParameter(@"creationTimeVal", Tasks.CreationTime);
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", Tasks.Email);
                    SQLiteParameter columnOridnalParam = new SQLiteParameter(@"columnOridnalVal", Tasks.Email);

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(titleParam);
                    command.Parameters.Add(descriptionParam);
                    command.Parameters.Add(dueDateParam);
                    command.Parameters.Add(creationTimeParam);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(columnOridnalParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //log error
                }
                finally
                {
                    command.Dispose();
                    connection.Close();

                }
                return res > 0;

            }
        }

    }
}
