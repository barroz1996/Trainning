using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class BoardEmailsControl:DalController
    {
      
        public BoardEmailsControl():base("BoardEmails")
        {
            
        }

        public List<DTOs.BoardEmailsDTO> SelectBoard(string Host) //Returns all columns with a specific email.
        {
            var emailList = new List<DTOs.BoardEmailsDTO>();
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = $"SELECT * FROM {_tableName} WHERE [{DTOs.BoardEmailsDTO.HostColumn}]=@Host";
                command.Parameters.Add(new SQLiteParameter(@"Host", Host));
                SQLiteDataReader dataReader = null;
                try
                {
                    connection.Open();
                    dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        emailList.Add(new DTOs.BoardEmailsDTO(dataReader.GetString(0), Host));
                    }
                }
                catch (Exception ex)
                {
                    log.Debug("an error occured while getting all emails from this board");
                    //have to check if put exception
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return emailList;
        }


       
        public bool Insert(DTOs.BoardEmailsDTO BoardEmail) //creates a new board in the database.
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand(connection);
                int res = -1;
                try
                {
                    connection.Open();
                    command.CommandText = $"INSERT INTO {_tableName}  ({DTOs.BoardEmailsDTO.EmailColumn},{DTOs.BoardEmailsDTO.HostColumn})  " +
                        $"VALUES (@EmailVal,@HostVal);";

                    var emailParam = new SQLiteParameter(@"EmailVal", BoardEmail.Email);
                    var hostParam = new SQLiteParameter(@"HostVal", BoardEmail.Host);
                    command.Parameters.Add(emailParam);
                    command.Parameters.Add(hostParam);
                    command.Prepare();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    log.Debug("an error occured while inserting a new boardEmail");
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
