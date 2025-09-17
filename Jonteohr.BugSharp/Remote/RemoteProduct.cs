namespace BugSharp.Remote
{
    public class RemoteProduct
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description  { get; set; }
        public bool is_active  { get; set; }
        public string default_milestone { get; set; }
        public bool has_unconfirmed { get; set; }
        public string classification { get; set; }
        public Component[] components { get; set; }
        public Version[] versions { get; set; }
        public Version[] milestones { get; set; }
    }
}