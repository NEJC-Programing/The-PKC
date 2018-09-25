using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPKC.API
{
    class Docs
    {
        public static void Save(DBEntry doc)
        {
            AddtoBD(doc);
        }
        public static void Save(string doc)
        {
            Save(new DBEntry(doc));
        }
        static DataIO db = new DataIO("local.db");

        static Server server = new Server("https://localhost");

        private static void AddtoBD(DBEntry doc)
        {
            db.AddDBentry(doc);
        }
        public static List<DBEntry> DBEntries { get { return db.DBEntries; } }

        public static void Sync()
        {
            Packed packed = new Packed();
            packed.VER = 0;
            packed.CMD = Commands.Write;

            packed.Data = DBEntries.ToArray();

            string data = server.SendJSON(packed.ToString());
        }
    }
}
