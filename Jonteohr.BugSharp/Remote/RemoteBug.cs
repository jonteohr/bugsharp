using System;
using System.Collections.Generic;
using Newtonsoft.Json;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BugSharp.Remote
{
    public class RemoteBug
    {
        public int Id { get; set; }
        
        public string Summary { get; set; }
        
        public string Status { get; set; }
        
        public string Priority { get; set; }
        
        public string Product { get; set; }
        
        public string Resolution { get; set; }
        
        public string qa_contact { get; set; }
        
        public string Version { get; set; }
        
        public List<string> Cc { get; set; }
        
        public string Platform { get; set; }
        
        public string Classification { get; set; }
        
        public bool is_open { get; set; }
        
        public string assigned_to { get; set; }
        
        public string Component  { get; set; }
        
        public bool IsConfirmed { get; set; }
        
        public DateTime creation_time { get; set; }
        
        public DateTime last_change_time { get; set; }
        
        public string Creator { get; set; }
        
        public string Severity { get; set; }
        public string target_milestone { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> CustomFields { get; set; } = new Dictionary<string, object>();
    }
}