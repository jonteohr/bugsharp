using System;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BugSharp.Remote
{
    public class RemoteAttachment
    {
        public DateTime last_change_time { get; set; }
        public string data { get; set; }
        public string content_type { get; set; }
        public int size { get; set; }
        public bool is_patch { get; set; }
        public int id { get; set; }
        public string summary { get; set; }
        public string file_name { get; set; }
        public bool is_obsolete { get; set; }
        public string creator { get; set; }
        public bool is_private { get; set; }
        public int bug_id { get; set; }
        public Flag[] flags { get; set; }
        public string comment { get; set; }
    }
}