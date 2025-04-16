#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace BugSharp.Remote
{
    public class RemoteTime
    {
        public string db_time { get; set; }
        public string web_time { get; set; }
        public string web_time_utc { get; set; }
        public string tz_name { get; set; }
        public string tz_short_name { get; set; }
        public string tz_offset { get; set; }
    }
}