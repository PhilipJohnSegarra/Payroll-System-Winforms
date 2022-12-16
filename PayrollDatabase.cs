using System;
using System.Data.SQLite;
using System.IO;

namespace PayrollGroup1
{
    class PayrollDatabase
    {
        public SQLiteConnection myConnection;

        public PayrollDatabase()
        {
            myConnection = new SQLiteConnection(@"Data Source=Databse\PayrollDatabase.db");
            

        }
    }
}
