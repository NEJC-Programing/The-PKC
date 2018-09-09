using Newtonsoft.Json;

namespace TPKC.API
{
    struct Packed
    {
        public DBEntry[] Data = null;
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
        public string Title;
        public string Body;
        public string Author;
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
