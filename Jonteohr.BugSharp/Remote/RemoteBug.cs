using System;
using System.Collections.Generic;
using Newtonsoft.Json;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BugSharp.Remote
{
    public class RemoteBug
    {
        public int id { get; set; }
        
        public string summary { get; set; }
        
        public string status { get; set; }
        
        public string priority { get; set; }
        
        public string product { get; set; }
        
        public string resolution { get; set; }
        
        public string qa_contact { get; set; }
        
        public string version { get; set; }
        
        public List<string> cc { get; set; }
        
        public string platform { get; set; }
        
        public string classification { get; set; }
        
        public bool is_open { get; set; }
        
        public string assigned_to { get; set; }
        
        public string component  { get; set; }
        
        public bool is_confirmed { get; set; }
        
        public DateTime creation_time { get; set; }
        
        public DateTime last_change_time { get; set; }
        
        public string creator { get; set; }
        
        public string severity { get; set; }
        public string target_milestone { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> CustomFields { get; set; } = new Dictionary<string, object>();
    }
}