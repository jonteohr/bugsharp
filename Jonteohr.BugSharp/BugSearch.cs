using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace BugSharp
{
    /// <summary>
    /// A object used for searching bugs
    /// </summary>
    public class BugSearch
    {
        private readonly BugZilla _bugZilla;

        /// <summary>
        /// Create a new BugSearch
        /// </summary>
        /// <param name="bugZilla">The bugzilla instance that owns this search</param>
        public BugSearch(BugZilla bugZilla)
        {
            _bugZilla = bugZilla;
        }
        
        /// <summary>
        /// The unique aliases of this bug. An empty array will be returned if this bug has no aliases.
        /// </summary>
        public List<string> Alias { get; set; }
        
        /// <summary>
        /// The login name of a user that a bug is assigned to.
        /// </summary>
        [QueryName("assigned_to")]
        public string AssignedTo { get; set; }
        
        /// <summary>
        /// The name of the Component that the bug is in. Note that if there are multiple Components with the same name, and you search for that name, bugs in all those Components will be returned. If you don’t want this, be sure to also specify the product argument.
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// Searches for bugs that were created at this time or later. May not be an array.
        /// </summary>
        [QueryName("creation_time")]
        public DateTime CreationTime { get; set; }
        
        /// <summary>
        /// The login name of the user who created the bug. You can also pass this argument with the name reporter, for backwards compatibility with older Bugzillas.
        /// </summary>
        public string Creator { get; set; }
        
        /// <summary>
        /// The numeric ID of the bug.
        /// </summary>
        public int[] Id { get; set; }
        
        /// <summary>
        /// Searches for bugs that were modified at this time or later. May not be an array.
        /// </summary>
        [QueryName("last_change_time")]
        public DateTime LastChangeTime { get; set; }
        
        /// <summary>
        /// Limit the number of results returned. If the limit is more than zero and higher than the maximum limit set by the administrator, then the maximum limit will be used instead. If you set the limit equal to zero, then all matching results will be returned instead.
        /// </summary>
        public int Limit { get; set; }
        
        /// <summary>
        /// Used in conjunction with the limit argument, offset defines the starting position for the search. For example, given a search that would return 100 bugs, setting limit to 10 and offset to 10 would return bugs 11 through 20 from the set of 100.
        /// </summary>
        public int Offset { get; set; }
        
        /// <summary>
        /// The “Operating System” field of a bug.
        /// </summary>
        [QueryName("op_sys")]
        public string OpSys { get; set; }
        
        /// <summary>
        /// The Platform (sometimes called “Hardware”) field of a bug.
        /// </summary>
        public string Platform { get; set; }
        
        /// <summary>
        /// The Priority field on a bug.
        /// </summary>
        public string Priority { get; set; }
        
        /// <summary>
        /// The name of the Product that the bug is in.
        /// </summary>
        public string Product { get; set; }
        
        /// <summary>
        /// The current resolution–only set if a bug is closed. You can find open bugs by searching for bugs with an empty resolution.
        /// </summary>
        public string Resolution { get; set; }
        
        /// <summary>
        /// The Severity field on a bug.
        /// </summary>
        public string Severity { get; set; }
        
        /// <summary>
        /// The current status of a bug (not including its resolution, if it has one, which is a separate field above).
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// Searches for substrings in the single-line Summary field on bugs. If you specify an array, then bugs whose summaries match any of the passed substrings will be returned. Note that unlike searching in the Bugzilla UI, substrings are not split on spaces. So searching for foo bar will match “This is a foo bar” but not “This foo is a bar”. ['foo', 'bar'], would, however, match the second item.
        /// </summary>
        public string Summary { get; set; }
        
        /// <summary>
        /// Searches for a bug with the specified tag. If you specify an array, then any bugs that match any of the tags will be returned. Note that tags are personal to the currently logged in user.
        /// </summary>
        public string Tags { get; set; }
        
        /// <summary>
        /// The Target Milestone field of a bug. Note that even if this Bugzilla does not have the Target Milestone field enabled, you can still search for bugs by Target Milestone. However, it is likely that in that case, most bugs will not have a Target Milestone set (it defaults to “—” when the field isn’t enabled).
        /// </summary>
        [QueryName("target_milestone")]
        public string TargetMilestone { get; set; }
        
        /// <summary>
        /// The login name of the bug’s QA Contact. Note that even if this Bugzilla does not have the QA Contact field enabled, you can still search for bugs by QA Contact (though it is likely that no bug will have a QA Contact set, if the field is disabled).
        /// </summary>
        [QueryName("qa_contact")]
        public string QaContact { get; set; }
        
        /// <summary>
        /// The “URL” field of a bug.
        /// </summary>
        public string Url { get; set; }
        
        /// <summary>
        /// The Version field of a bug.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// Search the “Status Whiteboard” field on bugs for a substring. Works the same as the summary field described above, but searches the Status Whiteboard field.
        /// </summary>
        public string Whiteboard { get; set; }
        
        /// <summary>
        /// Search for bugs using quicksearch syntax.
        /// </summary>
        public string QuickSearch { get; set; }
        
        internal string ToQueryString()
        {
            var properties = this.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var keyValuePairs = new List<string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(this);
                
                // Skip nulls and empty strings
                if (value == null)
                    continue;
        
                // Skip empty strings
                if (value is string strValue && string.IsNullOrWhiteSpace(strValue))
                    continue;
        
                // Skip DateTime.MinValue or default(DateTime)
                if (value is DateTime dtValue && dtValue == default(DateTime))
                    continue;

                // Skip unset integers
                if (value is int intValue && intValue == 0)
                    continue;

                // Convert array to URL-string
                if (value is int[] intArrayValue && intArrayValue.Length != 0)
                {
                    value = string.Join(",", intArrayValue);
                }
                else // If empty, skip it
                {
                    continue;
                }
                
                // Check for the custom QueryNameAttribute
                var attr = prop.GetCustomAttribute<QueryNameAttribute>();
                var name = attr?.Name ?? prop.Name.ToLower(); // Use attribute name or default to lowercase property name
        
                // Encode and add to list
                var encodedKey = WebUtility.UrlEncode(name);
                var encodedValue = WebUtility.UrlEncode(value.ToString());
        
                keyValuePairs.Add($"{encodedKey}={encodedValue}");
            }

            return string.Join("&", keyValuePairs);
        }

        /// <summary>
        /// Search for bugs matching a query
        /// </summary>
        /// <returns>List of bugs</returns>
        public async Task<List<Bug>> SearchBugsAsync()
        {
            return await _bugZilla.Bugs.SearchBugsAsync(this);
        }
    }
}