using System;
using System.Data.SQLite;
using System.IO;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    internal class SQLCreator
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly string _connectionString;
        private readonly string path;
        public SQLCreator() //constructor calls create method.
        {
            path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            _connectionString = $"Data Source={path}; Version=3;";
            Create();
        }
        public void Create()
        {
            if (!File.Exists(path)) //if the file does not exist, it will create a new db file with all the tables.
            {
                SQLiteConnection.CreateFile("database.db");

                using (var connection = new SQLiteConnection(_connectionString))
                {
                    var BoardEmails= "CREATE TABLE \"BoardEmails\"(\"Email\" TEXT NOT NULL UNIQUE,\"Host\"  TEXT NOT NULL,PRIMARY KEY(\"Email\"))";
                    var UserTable = "CREATE TABLE \"Users\"( \"Email\" TEXT NOT NULL UNIQUE, \"Nickname\"  TEXT NOT NULL, \"Password\"  TEXT NOT NULL , \"LoggedIn\"  INTEGER NOT NULL,\"EmailHost\"  TEXT NOT NULL,PRIMARY KEY (\"Email\"))";
                    var BoardTable = "CREATE TABLE \"Boards\" (\"Email\" TEXT NOT NULL UNIQUE,\"DeletedTasks\" INTEGER NOT NULL, PRIMARY KEY(\"Email\"))";
                    var ColumnTable = "CREATE TABLE \"Columns\" (\"ColumnOrdinal\" INTEGER NOT NULL,\"ColumnName\"    TEXT NOT NULL,\"ColumnLimit\" INTEGER NOT NULL,\"Email\" TEXT NOT NULL,PRIMARY KEY(\"ColumnOrdinal\", \"Email\"))";
                    var TaskTable = "CREATE TABLE \"Tasks\"(\"ID\"    INTEGER NOT NULL UNIQUE, \"Title\" TEXT NOT NULL, \"Description\"   TEXT,\"DueDate\"   TEXT NOT NULL, \"CreationDate\"  TEXT NOT NULL, \"Email\" TEXT NOT NULL, \"ColumnOrdinal\" INTEGER NOT NULL,\"EmailAssignee\" TEXT NOT NULL, PRIMARY KEY(\"ID\"))";
                    var command = new SQLiteCommand(null, connection);

                    try //Creates all tables.
                    {
                        connection.Open();
                        command.CommandText = UserTable;
                        command.ExecuteNonQuery();
                        command.CommandText = BoardTable;
                        command.ExecuteNonQuery();
                        command.CommandText = ColumnTable;
                        command.ExecuteNonQuery();
                        command.CommandText = TaskTable;
                        command.ExecuteNonQuery();
                        command.CommandText = BoardEmails;
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

            }

        }
    }
}
