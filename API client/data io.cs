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

            DB = new SQLiteConnection("Data Source=" + DatabasePath + ";Version=3;");

            if (n)
            {
                RunSQL(@"CREATE TABLE LOCAL(
                ID INT PRIMARY KEY NOT NULL AUTOINCREMENT UNIQUE,
                TITLE TEXT NOT NULL UNIQUE,
                BODY TEXT NOT NULL UNIQUE,
                AUTHERID INT NOT NULL
                ");

            }

        }

        public void RunSQLQuery(string sql)
        {
            DB.Open();
            //new SQLiteCommand(sql, DB).();
            DB.Close();
        }

        public void RunSQL(string sql)
        {
            DB.Open();
            new SQLiteCommand(sql, DB).ExecuteNonQuery();
            DB.Close();
        }
    }
}

