using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;

namespace IntroSE.Kanban.Backend.DataAccessLayer.Controllers
{
    class sqlCreator
    {
        private readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _connectionString;
        private string path;
        public sqlCreator()
        {
            this.path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "database.db"));
            this._connectionString = $"Data Source={path}; Version=3;";
            Create();
        }
        public void Create()
        {
            if (!File.Exists(path))
            {
                SQLiteConnection.CreateFile("database.db");

                using (var connection = new SQLiteConnection(_connectionString))
                {

                    string UserTable = "CREATE TABLE \"Users\"( \"Email\" TEXT NOT NULL UNIQUE, \"Nickname\"  TEXT NOT NULL, \"Password\"  TEXT NOT NULL, \"LoggedIn\"  INTEGER NOT NULL,PRIMARY KEY (\"Email\"))";
                    //string BoardTable = @"CREATE TABLE cars(id INTEGER PRIMARY KEY,name TEXT, price INT)";
                    string BoardTable = "CREATE TABLE \"Boards\" (\"Email\" TEXT NOT NULL UNIQUE, PRIMARY KEY(\"Email\"))";
                    string ColumnTable = "CREATE TABLE \"Columns\" (\"ColumnOrdinal\" INTEGER NOT NULL,\"ColumnName\"    TEXT NOT NULL,\"ColumnLimit\" INTEGER NOT NULL,\"Email\" TEXT NOT NULL,PRIMARY KEY(\"ColumnOrdinal\", \"Email\"))";
                    string TaskTable = "CREATE TABLE \"Tasks\"(\"ID\"    INTEGER NOT NULL UNIQUE, \"Title\" TEXT NOT NULL, \"Description\"   TEXT,\"DueDate\"   TEXT NOT NULL, \"CreationDate\"  TEXT NOT NULL, \"Email\" TEXT NOT NULL, \"ColumnOrdinal\" INTEGER NOT NULL, PRIMARY KEY(\"ID\"))";
                    SQLiteCommand command = new SQLiteCommand(null, connection);

                    try
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
