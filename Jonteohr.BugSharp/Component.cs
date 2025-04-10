using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BugSharp
{
    /// <summary>
    /// A BugZilla component
    /// </summary>
    public class Component
    {
        private readonly BugZilla _bugZilla;
        
        /// <summary>
        /// Create a new bug
        /// </summary>
        /// <param name="bugZilla">BugZilla instance that owns this bug</param>
        public Component(BugZilla bugZilla)
        {
            _bugZilla = bugZilla;
        }

        /// <summary>
        /// The name of the product that the component must be added to. This product must already exist, and the user have the necessary permissions to edit components for it.
        /// </summary>
        public string Product { get; set; }
        
        /// <summary>
        /// The name of the new component.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// The description of the new component.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// The login name of the default assignee of the component.
        /// </summary>
        [JsonProperty("default_assignee")]
        public string DefaultAssignee { get; set; }
        
        /// <summary>
        /// Each string representing one login name of the default CC list.
        /// </summary>
        [JsonProperty("default_cc")]
        public string[] DefaultCC { get; set; }
        
        /// <summary>
        /// The login name of the default QA contact for the component.
        /// </summary>
        [JsonProperty("default_qa_contact")]
        public string DefaultQAContact { get; set; }

        /// <summary>
        /// true if you want to enable the component for bug creations. false otherwise. Default is true.
        /// </summary>
        [JsonProperty("is_open")]
        public bool IsOpen { get; set; }

        /// <summary>
        /// Saves the field changes to server
        /// </summary>
        /// <returns>The created Component id if successful</returns>
        public async Task<int> SaveChangesAsync()
        {
            return await _bugZilla.Components.CreateComponentAsync(this);
        }
    }
}