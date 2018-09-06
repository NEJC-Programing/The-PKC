using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TPKC.API
{
    class DataIO
    {
        public SQLiteConnection DB;

        public DataIO(string DatabasePath)
        {
            bool n = false;
            if (!File.Exists(DatabasePath))
            {
                SQLiteConnection.CreateFile(DatabasePath);
                n = true;
            }

            DB = new SQLiteConnection(@"Data Source=" + DatabasePath + ";Version=3;");
            
            if(n)
            {
                
            }

        }

        public void RunSQL(string sql)
        {
            DB.Open();
            new SQLiteCommand(sql, DB).ExecuteNonQuery();
            DB.Close();
        }
    }
}
