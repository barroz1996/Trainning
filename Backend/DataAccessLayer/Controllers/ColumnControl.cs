using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class ColumnControl
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _connectionString;
        private readonly string _tableName;
        public ColumnControl()
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            this._tableName = "Columns";
        }

        public bool Update(int id, string attributeName, string attributeValue,string email)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET [{attributeName}]=@{attributeName} WHERE id={id} AND email ={email}"
                };
                try
                {

                    command.Parameters.Add(new SQLiteParameter(attributeName, attributeValue));
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch(Exception ex)
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

        public bool Update(int id, string attributeName, int attributeValue,string email)
        {
            int res = -1;
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"UPDATE {_tableName} SET [{attributeName}]=@{attributeName} WHERE id={id} AND email ={email}"
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

        public List<DTOs.ColumnDTO> SelectColumn(string email)
        {
            List<DTOs.ColumnDTO> columnsList = new List<DTOs.ColumnDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = $"select* from {_tableName} where [{DTOs.ColumnDTO.ColumnEmailColumnEmail}]=@Email";
                command.Parameters.Add(new SQLiteParameter(@"Email", email));
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        columnsList.Add(new DTOs.ColumnDTO((int)dataReader.GetValue(0),dataReader.GetString(1),(int)dataReader.GetValue(2),email));
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
            return columnsList;
        }
        public bool DeleteTable()
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {_tableName} "
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

        public bool Delete(string email,int columnOrdinal)
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"Delete FROM {_tableName} WHERE [{DTOs.ColumnDTO.ColumnEmailColumnEmail}]=@Email AND [{DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal}]=@colOrdinal"
                };
                SQLiteParameter emailParam = new SQLiteParameter(@"Email", email);
                SQLiteParameter colOrdinalParam = new SQLiteParameter(@"colOrdinal", columnOrdinal);
                command.Parameters.Add(emailParam);
                command.Parameters.Add(colOrdinalParam);
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
        public bool Insert(DTOs.ColumnDTO Columns)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName}  ({DTOs.ColumnDTO.ColumnOrdinalColumnOrdinal} ,{DTOs.ColumnDTO.ColumnNameColumnName},{DTOs.ColumnDTO.ColumnLimitColumnLimit},{DTOs.ColumnDTO.ColumnEmailColumnEmail}) " +
                        $"VALUES (@columnOridnalVal,@columnNameVal,@limitVal,@email);";

                    SQLiteParameter idParam = new SQLiteParameter(@"columnOridnalVal", Columns.ColumnOrdinal);
                    SQLiteParameter NameParam = new SQLiteParameter(@"columnNameVal", Columns.ColumnName);
                    SQLiteParameter limitParam = new SQLiteParameter(@"limitVal", Columns.Limit);
                    SQLiteParameter emailParam = new SQLiteParameter(@"email", Columns.Email);
                    

                    command.Parameters.Add(idParam);
                    command.Parameters.Add(NameParam);
                    command.Parameters.Add(limitParam);
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
