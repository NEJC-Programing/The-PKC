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
        }
        public DBEntry(string JSON)
        {
            this = JsonConvert.DeserializeObject<DBEntry>(JSON);
        }
        public int ID { get; private set; } // the id
        public string Title { get; set; } // text
        public string Body { get; set; } // html
        public string Author { get; set; } // text
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
