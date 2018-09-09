using Newtonsoft.Json;
using System.Net;

namespace TPKC.API
{
    class Server
    {
        public string ServerAddress;

        public Server(string address)
        {
            ServerAddress = address;
        }

        public string SendJSON(string json)
        {
            return new WebClient().UploadString(ServerAddress, json);
        }
    }
}
