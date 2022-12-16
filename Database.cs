using System;
using System.Data.SQLite;
using System.IO;
namespace PayrollGroup1
{
    class Database
    {
        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection(@"Data Source=Databse\Database.db");
            
            
        }
    }
}
