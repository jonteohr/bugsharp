using System;

namespace BugSharp.Remote
{
    public class RemoteComment
    {
        public DateTime time { get;set; }
        public string text { get; set; }
        public int bug_id { get; set; }
        public int count { get; set; }
        public int? attachment_id { get; set; }
        public bool is_private { get; set; }
        public string[] tags { get; set; }
        public string creator { get; set; }
        public DateTime creation_time { get; set; }
        public int id { get; set; }
    }
}