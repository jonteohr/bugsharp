using Newtonsoft.Json;

namespace BugSharp
{
    public class Version
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        [JsonProperty("sort_key")]
        public int SortKey { get; set; }
    }
}