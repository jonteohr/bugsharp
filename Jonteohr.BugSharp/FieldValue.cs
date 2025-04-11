using System.Collections.Generic;
using Newtonsoft.Json;

namespace BugSharp
{
    /// <summary>
    /// The main field value object class
    /// </summary>
    public class FieldValue
    {
        /// <summary>
        /// The actual value–this is what you would specify for this field in create, etc.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Values, when displayed in a list, are sorted first by this integer and then secondly by their name.
        /// </summary>
        [JsonProperty("sort_key")]
        public int SortKey { get; set; }
        
        /// <summary>
        /// If value_field is defined for this field, then this value is only shown if the value_field is set to one of the values listed in this array. Note that for per-product fields, value_field is set to product and visibility_values will reflect which product(s) this value appears in.
        /// </summary>
        [JsonProperty("visibility_values")]
        public List<string> VisibilityValues { get; set; }
        
        /// <summary>
        /// This value is defined only for certain product-specific fields such as version, target_milestone or component. When true, the value is active; otherwise the value is not active.
        /// </summary>
        [JsonProperty("is_active")]
        public bool IsActive { get; set; }
        
        /// <summary>
        /// The description of the value. This item is only included for the keywords field.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// For bug_status values, determines whether this status specifies that the bug is “open” (true) or “closed” (false). This item is only included for the bug_status field.
        /// </summary>
        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }
        
        /// <summary>
        /// For bug_status values, this is an array of objects that determine which statuses you can transition to from this status. (This item is only included for the bug_status field.) <br />
        /// Each object contains the following items:
        /// <list type="bullet">
        /// <item><b>name:</b> <i>(string)</i> The name of the new status</item>
        /// <item><b>comment_required:</b> <i>(boolean)</i> true if a comment is required when you change a bug into this status using this transition.</item>
        /// </list>
        /// </summary>
        [JsonProperty("can_change_to")]
        public object[] CanChangeTo { get; set; }
    }
}