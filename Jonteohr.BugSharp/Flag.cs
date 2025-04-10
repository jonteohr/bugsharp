using System;
using Newtonsoft.Json;

namespace BugSharp
{
    /// <summary>
    /// The flag object for attachment flags
    /// </summary>
    public class Flag
    {
        /// <summary>
        /// The ID of the flag.
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// The name of the flag.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The type ID of the flag.
        /// </summary>
        [JsonProperty("type_id")]
        public int TypeId { get; set; }
        
        /// <summary>
        /// The timestamp when this flag was originally created.
        /// </summary>
        [JsonProperty("creation_date")]
        public DateTime CreationDate { get; set; }
        
        /// <summary>
        /// The timestamp when the flag was last modified.
        /// </summary>
        [JsonProperty("modification_date")]
        public DateTime ModificationDate { get; set; }
        
        /// <summary>
        /// The current status of the flag such as ?, +, or -.
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// The login name of the user who created or last modified the flag.
        /// </summary>
        public string Setter { get; set; }
        
        /// <summary>
        /// The login name of the user this flag has been requested to be granted or denied. Note, this field is only returned if a requestee is set.
        /// </summary>
        public string Requestee { get; set; }
    }
}