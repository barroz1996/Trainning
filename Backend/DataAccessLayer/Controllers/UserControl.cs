using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class UserControl
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _connectionString;
        private readonly string _tableName;
        public UserControl()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = "Users";
        }

        public bool Update(string id, string attributeName, string attributeValue)
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

        public bool Update(string id, string attributeName, bool attributeValue)//we need to check!
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
                    command.ExecuteNonQuery();
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

        public List<DTOs.UserDTO> Select()
        {
            List<DTOs.UserDTO> userList = new List<DTOs.UserDTO>();
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
                        userList.Add(new DTOs.UserDTO(dataReader.GetString(0),dataReader.GetString(1),dataReader.GetString(2),dataReader.GetBoolean(3)));
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
            return userList;
        }

        public bool Delete(DTOs.UserDTO DTOObj)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {_tableName} WHERE [{DTOs.UserDTO.UsersEmailColumn}]=@emailVal"
                };
                command.Parameters.Add(new SQLiteParameter(@"emailVal", DTOObj.Email));
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
        public bool Insert(DTOs.UserDTO User)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName}  ({DTOs.UserDTO.UsersEmailColumn} ,{DTOs.UserDTO.UsersNicknameColumn},{DTOs.UserDTO.UsersPasswordColumn},{DTOs.UserDTO.UsersLoggedInColumn}) " +
                        $"VALUES (@emailVal,@nickNameVal,@passwordVal,@loggedInVal);";

                    command.Parameters.Add(new SQLiteParameter(@"emailVal", User.Email));
                    command.Parameters.Add(new SQLiteParameter(@"nickNameVal", User.Nickname));
                    command.Parameters.Add(new SQLiteParameter(@"passwordVal", User.Password));
                    command.Parameters.Add(new SQLiteParameter(@"loggedInVal", User.LoggedIn));
                    
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
