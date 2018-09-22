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
        public DBEntry(string DocTitle, string DocBody, string DocAuthor)
        {
            Title = DocTitle;
            Body = DocBody;
            Author = DocAuthor;
        }
        public string Title { get; private set; } // text
        public string Body { get; private set; } // html
        public string Author { get; private set; } // text
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
