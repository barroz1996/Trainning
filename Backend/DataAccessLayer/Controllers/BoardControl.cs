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
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                command.CommandText = $"SELECT * FROM {_tableName}";
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        boardsList.Add(new DTOs.BoardDTO(dataReader.GetString(0)));
                    }
                    
                }
                catch (Exception ex)
                {
                    log.Debug(ex.Message);
                }
                finally
                {
                    if (dataReader == null)
                    {
                        dataReader.Close();
                    }
                    command.Dispose();
                    connection.Close();
                }
               

            }
            return boardsList;
        }

        public bool Delete(DTOs.BoardDTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {_tableName} WHERE [{DTOs.BoardDTO.BoardEmailColumnEmail}]=@Email"
                };
                SQLiteParameter emailParam = new SQLiteParameter(@"Email", DTOObj.Email);
                command.Parameters.Add(emailParam);
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Debug(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool DeleteTable()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    //CommandText = $"DELETE FROM {_tableName} "
                    CommandText = $"DROP TABLE {_tableName} "
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Debug(ex.Message);
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
        public bool Insert(DTOs.BoardDTO Board)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName} WHERE Email={DTOs.BoardDTO.BoardEmailColumnEmail}, ({DTOs.TaskDTO.TasksColumnIdColumnColumnId}  " +
                        $"VALUES (@EmailVal);";

                    SQLiteParameter emailParam = new SQLiteParameter(@"EmailVal", Board.Email);
                    //check later

                    command.Parameters.Add(emailParam);
                    
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Debug(ex.Message);
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
