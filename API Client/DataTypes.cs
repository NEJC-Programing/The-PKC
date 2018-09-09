using Newtonsoft.Json;

namespace TPKC.API
{
    struct Packed
    {
        public int cmd;
        public float ver;
        public DBEntry[] Data;
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
        public string Title; // text
        public string Body; // html
        public string Author; // text
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
