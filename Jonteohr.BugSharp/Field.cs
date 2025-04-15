using System.Collections.Generic;
using Newtonsoft.Json;

namespace BugSharp
{
    /// <summary>
    /// The main field object class
    /// </summary>
    public class Field
    {
        /// <summary>
        /// An integer ID uniquely identifying this field in this installation only.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The number of the fieldtype. The following values are defined:
        /// <list>
        /// <item>0. Field type unknown</item>
        /// <item>1. Single-line string field</item>
        /// <item>2. Single value field</item>
        /// <item>3. Multiple value field</item>
        /// <item>4. Multi-line text value</item>
        /// <item>5. Date field with time</item>
        /// <item>6. Bug ID field</item>
        /// <item>7. See Also field</item>
        /// <item>8. Keywords field</item>
        /// <item>9. Date field</item>
        /// <item>10. Integer field</item>
        /// </list>
        /// </summary>
        public int Type { get; set; }
        
        /// <summary>
        /// <b>true</b> when this is a custom field, <b>false</b> otherwise.
        /// </summary>
        [JsonProperty("is_custom")]
        public bool IsCustom { get; set; }
        
        /// <summary>
        /// The internal name of this field. This is a unique identifier for this field. If this is not a custom field, then this name will be the same across all Bugzilla installations.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The name of the field, as it is shown in the user interface.
        /// </summary>
        [JsonProperty("display_name")]
        public string DisplayName { get; set; }
        
        /// <summary>
        /// <b>true</b> if the field must have a value when filing new bugs. Also, mandatory fields cannot have their value cleared when updating bugs.
        /// </summary>
        [JsonProperty("is_mandatory")]
        public bool IsMandatory { get; set; }
        
        /// <summary>
        /// For custom fields, this is <b>true</b><br /> if the field is shown when you enter a new bug. For standard fields, this is currently always <b>false</b>, even if the field shows up when entering a bug.
        /// </summary>
        [JsonProperty("is_on_bug_entry")]
        public bool IsOnBugEntry { get; set; }
        
        /// <summary>
        /// The name of a field that controls the visibility of this field in the user interface. This field only appears in the user interface when the named field is equal to one of the values is visibility_values. Can be null.
        /// </summary>
        [JsonProperty("visibility_field")]
        public string VisibilityField { get; set; }
        
        /// <summary>
        /// This field is only shown when visibility_field matches one of these string values. When visibility_field is null, then this is an empty array.
        /// </summary>
        /// <returns></returns>
        [JsonProperty("visibility_values")]
        public List<FieldValue> VisibilityValues { get; set; }
        
        /// <summary>
        /// The name of the field that controls whether or not particular values of the field are shown in the user interface. Can be null.
        /// </summary>
        [JsonProperty("value_field")]
        public string ValueField { get; set; }
        
        /// <summary>
        /// Objects representing the legal values for select-type (drop-down and multiple-selection) fields. This is also populated for the component, version, target_milestone, and keywords fields, but not for the product field (you must use get_accessible_products for that). For fields that aren’t select-type fields, this will simply be an empty array. Each object contains the items described in the Value object below.
        /// </summary>
        public List<FieldValue> Values { get; set; }
    }
}