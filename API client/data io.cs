using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace TPKC.API
{
    class DataIO
    {
        public SQLiteConnection DB { get; private set; }

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
                AUTHER TEXT NOT NULL
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

        public void AddDBentry(DBEntry entry)
        {
            //RunSQL(@"INSERT INTO LOCAL(TITLE, BODY, AUTHERID) VALUES('','','')");
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"INSERT INTO LOCAL(TITLE, BODY, AUTHER) VALUES(@Title,@Body,@Auther)";
            cmd.Parameters.Add(new SQLiteParameter("@Title", entry.Title));
            cmd.Parameters.Add(new SQLiteParameter("@Body", entry.Body));
            cmd.Parameters.Add(new SQLiteParameter("@Auther", entry.Author));
            SQLiteDataReader reader = cmd.ExecuteReader();
            DB.Close();
            reader.Close();
        }

        public List<DBEntry> DBEntries {
            get {
                DB.Open();
                SQLiteCommand cmd = DB.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = @"SELECT * FROM LOCAL";
                List<DBEntry> buff = new List<DBEntry>();

                using (SQLiteDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        int id = (int)rdr["ID"];
                        string title = (string)rdr["TITLE"];
                        string doc = (string)rdr["BODY"];
                        string auther = (string)rdr["AUTHER"];

                        buff.Add(new DBEntry(title,doc,auther,id));
                    }
                    rdr.Close();
                }
                DB.Close();
                return buff;
            }
        }
    }
}

