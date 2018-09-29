using Newtonsoft.Json;

namespace TPKC.API
{
    enum Commands
    {
        Read,
        Write
    }
    struct Packed
    {

        public Commands CMD { get; set; }
        public double VER { get; set; }
        public DBEntry[] Data { get; set; }
        public int[] DOCS { get; set; }
        
        /// <summary>
        /// Loads Packed from json that came from server
        /// </summary>
        /// <param name="JSON">JSON value of a Packed</param>
        public Packed(string JSON)
        {
            this = JsonConvert.DeserializeObject<Packed>(JSON);
        }

        /// <summary>
        /// Gets the JSON Data of This Packed
        /// </summary>
        /// <returns>JOSN Value of Packed</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(Data);
        }
    }
}

namespace TPKC
{
    
    struct DBEntry
    {
        public DBEntry(string DocTitle, string DocBody, string DocAuthor, int DocID = 0)
        {
            
            ID = DocID;
            Title = DocTitle;
            Body = DocBody;
            Author = DocAuthor;
            if (ID < 1)
                ID = GetNextID();
        }
        public DBEntry(string JSON)
        {
            this = JsonConvert.DeserializeObject<DBEntry>(JSON);
            if (ID < 1)
                ID = GetNextID();
        }
        
        private int GetNextID()
        {
            return 0;
        }

        public int ID { get; private set; } // the id
        public string Title { get; set; } // text
        public string Body { get; set; } // html
        public string Author { get; set; } // text

        public bool Changed { get { return false; } } // cheks agents server if the docs are the same

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return ((DBEntry)obj).ID == ID;
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
