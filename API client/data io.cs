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
                ID INT PRIMARY KEY NOT NULL UNIQUE,
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

        /// <summary>
        /// add entry to the database
        /// </summary>
        /// <param name="entry">the entry to add</param>
        public void AddDBentry(DBEntry entry)
        {
            DB.Open();
            SQLiteCommand cmd = DB.CreateCommand();
            cmd.CommandType = CommandType.Text;

            // check if id is in db if yes then update do not add
            if (DBEntries.Contains(entry))
            {
                cmd.CommandText = "UPDATE LOCAL SET TITLE = @Title, BODY = @Body, AUTHER = @Auther WHERE ID = @id";
                cmd.Parameters.Add(new SQLiteParameter("@Title", entry.Title));
                cmd.Parameters.Add(new SQLiteParameter("@Body", entry.Body));
                cmd.Parameters.Add(new SQLiteParameter("@Auther", entry.Author));
                cmd.Parameters.Add(new SQLiteParameter("@id", entry.ID));
            }
            else
            {
                cmd.CommandText = @"INSERT INTO LOCAL(ID, TITLE, BODY, AUTHER) VALUES(@id, @Title, @Body, @Auther)";
                cmd.Parameters.Add(new SQLiteParameter("@Title", entry.Title));
                cmd.Parameters.Add(new SQLiteParameter("@Body", entry.Body));
                cmd.Parameters.Add(new SQLiteParameter("@Auther", entry.Author));
                cmd.Parameters.Add(new SQLiteParameter("@id", entry.ID));
            }

            SQLiteDataReader reader = cmd.ExecuteReader();
            DB.Close();
            reader.Close();
        }

        public List<DBEntry> DBEntries {
            get
            {
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

