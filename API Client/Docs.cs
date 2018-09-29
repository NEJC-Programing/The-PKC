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
        public static void Save(string JSONdoc)
        {
            Save(new DBEntry(JSONdoc));
        }
        static DataIO db = new DataIO("local.db");

        static Server server = new Server("https://localhost");

        public static void AddtoBD(DBEntry doc)
        {
            db.AddDBentry(doc);
        }
        public static List<DBEntry> DBEntries { get { return db.DBEntries; } }

        public static void Sync()
        {
            Packed packed = new Packed();
            packed.VER = 0;
            packed.CMD = Commands.Write;

            List<int> docsWeHave = new List<int>();
            List<DBEntry> changeddocs = new List<DBEntry>();

            foreach (DBEntry entry in DBEntries)
            {
                docsWeHave.Add(entry.ID);
                if (entry.Changed)
                    changeddocs.Add(entry);
            }

            packed.Data = changeddocs.ToArray();
            packed.DOCS = docsWeHave.ToArray();

            string data = server.SendJSON(packed.ToString());

            packed = new Packed(data);

            foreach (DBEntry entry in packed.Data)
            {
                bool save = true;
                foreach (DBEntry intent in DBEntries)
                {
                    if (entry.Equals(intent))
                    {
                        save = false;
                        break;
                    }
                }
                if (save)
                    AddtoBD(entry);
            }
        }
    }
}
