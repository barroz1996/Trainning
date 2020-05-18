using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
 

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class TaskControl
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _connectionString;
        private readonly string _tableName;
        public TaskControl()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = "Tasks";
        }

        public bool Update(int ID, string attributeName, string attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET [{attributeName}]=@{attributeName} WHERE ID=@ID"
                };
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter(@"ID", ID));
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
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

            }
            return res > 0;
        }

        public bool Update(int ID, string attributeName, int attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET [{attributeName}]=@{attributeName} WHERE ID=@ID"
                };
                try
                {
                    connection.Open();
                    command.Parameters.Add(new SQLiteParameter(@"ID", ID));
                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
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

            }
            return res > 0;
        }
        public bool Update(int id, string attributeName, DateTime attributeValue)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET [{attributeName}]=@{attributeName} WHERE id={id}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
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

        public List<DTOs.TaskDTO> SelectTasks(string email, int ColumnOridnal)
        {
            List<DTOs.TaskDTO> taskList = new List<DTOs.TaskDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = $"SELECT * FROM {_tableName} WHERE [{DTOs.TaskDTO.TasksEmailColumnEmail}]=@Email AND [{DTOs.TaskDTO.TasksIdColumnId}]=@ColumnOridnal";
                command.Parameters.Add(new SQLiteParameter(@"Email", email));
                command.Parameters.Add(new SQLiteParameter(@"ColumnOridnal", ColumnOridnal));
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        taskList.Add(new DTOs.TaskDTO(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), dataReader.GetDateTime(3), dataReader.GetDateTime(4), email, ColumnOridnal));
                    }
                }
                catch (Exception ex)
                {
                    log.Debug(ex.Message);
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
            return taskList;
        }

        public bool Delete(DTOs.TaskDTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {_tableName} WHERE [{DTOs.TaskDTO.TasksIdColumnId}]=@taskId"
                };
                command.Parameters.Add(new SQLiteParameter(@"taskId", DTOObj.TaskId));
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
        public bool Insert(DTOs.TaskDTO Tasks)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName}  ({DTOs.TaskDTO.TasksIdColumnId} ,{DTOs.TaskDTO.TasksTitleColumnTitle},{DTOs.TaskDTO.TasksDescriptionColumnDescription},{DTOs.TaskDTO.TasksDueDateColumnDueDate},{DTOs.TaskDTO.TasksCreationDateColumnCreationDate},{DTOs.TaskDTO.TasksEmailColumnEmail},{DTOs.TaskDTO.TasksColumnIdColumnColumnId}) " +
                        $"VALUES (@idVal,@titleVal,@descriptionVal,@dueDateTimeVal,@creationTimeVal,@emailVal,@columnOridnalVal);";

                    SQLiteParameter idParam = new SQLiteParameter(@"idVal", Tasks.TaskId);
                    SQLiteParameter titleParam = new SQLiteParameter(@"titleVal", Tasks.Title);
                    SQLiteParameter descriptionParam = new SQLiteParameter(@"descriptionVal", Tasks.Description);
                    SQLiteParameter dueDateParam = new SQLiteParameter(@"dueDateTimeVal", Tasks.DueDate);
                    SQLiteParameter creationTimeParam = new SQLiteParameter(@"creationTimeVal", Tasks.CreationTime);
                    SQLiteParameter emailParam = new SQLiteParameter(@"emailVal", Tasks.Email);
                    SQLiteParameter columnOridnalParam = new SQLiteParameter(@"columnOridnalVal", Tasks.ColumnOridnal);

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
