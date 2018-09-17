using Newtonsoft.Json;
using System.Collections.Generic;

namespace TextJSON
{
    public class Xml
    {
        [JsonProperty("@version")]
        public string __invalid_name__version { get; set; }
    }

    public class Suggestion
    {
        [JsonProperty("@data")]
        public string __invalid_name__data { get; set; }
    }
    
    public class CompleteSuggestion
    {
        public Suggestion suggestion { get; set; }
    }
    
    public class Toplevel
    {
        public List<CompleteSuggestion> CompleteSuggestion { get; set; }
    }
    
    public class RootObject
    {
        [JsonProperty("?xml")]
        public Xml __invalid_name__xml { get; set; }
        public Toplevel toplevel { get; set; }
    }
}    