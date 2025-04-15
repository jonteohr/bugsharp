using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp
{
    /// <summary>
    /// A BugZilla bug
    /// </summary>
    public class Bug
    {
        private readonly BugZilla _bugZilla;
        private RemoteBug _originalBug;

        /// <summary>
        /// Create a new bug
        /// </summary>
        /// <param name="bugZilla">BugZilla instance that owns this bug</param>
        public Bug(BugZilla bugZilla) : this(bugZilla, new RemoteBug())
        {
        }
        
        /// <summary>
        /// Create a new bug
        /// </summary>
        /// <param name="bugzilla">BugZilla instance that owns this bug</param>
        /// <param name="remoteBug">The remote bug object</param>
        public Bug(BugZilla bugzilla, RemoteBug remoteBug)
        {
            _bugZilla = bugzilla;
            Initialize(remoteBug);
        }

        private void Initialize(RemoteBug remoteBug)
        {
            _originalBug = remoteBug;

            Id = remoteBug.id;
            Summary = remoteBug.summary;
            Status = remoteBug.status;
            Priority = remoteBug.priority;
            Product = remoteBug.product;
            Resolution = remoteBug.resolution;
            QAContact = remoteBug.qa_contact;
            Version = remoteBug.version;
            Cc = remoteBug.cc;
            Platform = remoteBug.platform;
            Classification = remoteBug.classification;
            IsOpen = remoteBug.is_open;
            AssignedTo = remoteBug.assigned_to;
            Component = remoteBug.component;
            IsConfirmed = remoteBug.is_confirmed;
            Created = remoteBug.creation_time;
            Changed = remoteBug.last_change_time;
            Creator = remoteBug.creator; // TODO Change this to a custom class with more information on the creator
            Severity = remoteBug.severity;
            TargetMilestone = remoteBug.target_milestone;
            CustomFields = new Dictionary<string, object>(remoteBug.CustomFields);
        }

        /// <summary>
        /// ID of the bug
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// The summary of the bug
        /// </summary>
        public string Summary { get; set; }
        
        /// <summary>
        /// The status of the bug
        /// </summary>
        public string Status { get; set; }
        
        /// <summary>
        /// The priority of the bug
        /// </summary>
        public string Priority { get; set; }
        
        /// <summary>
        /// The product assigned to the bug
        /// </summary>
        public string Product { get; set; }
        
        /// <summary>
        /// The resolution set for the bug
        /// </summary>
        public string Resolution { get; set; }
        
        /// <summary>
        /// The QA Contact assigned to the bug
        /// </summary>
        public string QAContact { get; set; }
        
        /// <summary>
        /// The version this bug was found on
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// A list of contacts that are notofied of any changes
        /// </summary>
        public List<string> Cc { get; set; }
        
        /// <summary>
        /// The platform this bug was found on
        /// </summary>
        public string Platform { get; set; }
        
        /// <summary>
        /// Classification set for this bug
        /// </summary>
        public string Classification { get; set; }
        
        /// <summary>
        /// If this bug is still open
        /// </summary>
        public bool IsOpen { get; set; }
        
        /// <summary>
        /// The user this bug is assigned to
        /// </summary>
        public string AssignedTo { get; set; }
        
        /// <summary>
        /// The component this bug regards to
        /// </summary>
        public string Component  { get; set; }
        
        /// <summary>
        /// If this bug has been confirmed yet
        /// </summary>
        public bool IsConfirmed { get; set; }
        
        /// <summary>
        /// The DateTime timestamp of when this bug was created
        /// </summary>
        public DateTime Created { get; set; }
        
        /// <summary>
        /// The DateTime timestamp of when this bug was last modified
        /// </summary>
        public DateTime Changed { get; set; }
        
        /// <summary>
        /// The user that created this bug
        /// </summary>
        public string Creator { get; set; }
        
        /// <summary>
        /// The severity of this bug
        /// </summary>
        public string Severity { get; set; }
        
        /// <summary>
        /// The target milestone this bug is set to be resolved
        /// </summary>
        public string TargetMilestone { get; set; }

        /// <summary>
        /// Dictionary of any custom fields added to the bug.
        /// </summary>
        public Dictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Saves the field changes to server
        /// </summary>
        /// <returns>The updated bug from the server</returns>
        public async Task<Bug> SaveChangesAsync()
        {
            Bug serverBug;
            if (string.IsNullOrEmpty(_originalBug.id.ToString()))
            {
                var newKey = await _bugZilla.Bugs.CreateBugAsync(this);
                serverBug = await _bugZilla.Bugs.GetBugAsync(newKey);
            }
            else
            {
                await _bugZilla.Bugs.UpdateBugAsync(this);
                serverBug = await _bugZilla.Bugs.GetBugAsync(_originalBug.id);
            }
            
            return serverBug;
        }

        internal string SerializeChanges()
        {
            var changes = CompareToRemote();
            if (changes.Count == 0)
                return string.Empty;

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            var json = JsonConvert.SerializeObject(changes, jsonSettings);

            return json;
        }

        internal RemoteBug ToRemoteBug()
        {
            return new RemoteBug
            {
                id = Id,
                summary = Summary,
                status = Status,
                priority = Priority,
                product = Product,
                resolution = Resolution,
                assigned_to = AssignedTo,
                creation_time = Created,
                is_open = IsOpen,
                last_change_time = Changed,
                qa_contact = QAContact,
                target_milestone = TargetMilestone,
                cc = Cc,
                classification = Classification,
                component = Component,
                is_confirmed = IsConfirmed,
                creator = Creator,
                platform = Platform,
                severity = Severity,
                CustomFields = CustomFields,
                version = Version
            };
        }

        private Dictionary<string, object> CompareToRemote()
        {
            var diffs = new Dictionary<string, object>();

            void Compare<T>(T current, T original, string remoteName)
            {
                if (!EqualityComparer<T>.Default.Equals(current, original))
                {
                    diffs[remoteName] = current;
                }
            }

            Compare(Summary, _originalBug.summary, "summary");
            Compare(Status, _originalBug.status, "status");
            Compare(Priority, _originalBug.priority, "priority");
            Compare(Product, _originalBug.product, "product");
            Compare(Resolution, _originalBug.resolution, "resolution");
            Compare(QAContact, _originalBug.qa_contact, "qa_contact");
            Compare(Version, _originalBug.version, "version");

            if (!AreListsEqual(Cc, _originalBug.cc))
                diffs["cc"] = Cc;

            Compare(Platform, _originalBug.platform, "platform");
            Compare(Classification, _originalBug.classification, "classification");
            Compare(IsOpen, _originalBug.is_open, "is_open");
            Compare(AssignedTo, _originalBug.assigned_to, "assigned_to");
            Compare(Component, _originalBug.component, "component");
            Compare(IsConfirmed, _originalBug.is_confirmed, "is_confirmed");
            Compare(Creator, _originalBug.creator, "creator");
            Compare(Severity, _originalBug.severity, "severity");
            Compare(TargetMilestone, _originalBug.target_milestone, "target_milestone");

            if (_originalBug.CustomFields == null && CustomFields != null)
            {
                foreach (var kvp in CustomFields)
                {
                    diffs[kvp.Key] = kvp.Value;
                }
            }
            else if (CustomFields != null)
            {
                foreach (var kvp in CustomFields)
                {
                    if (!_originalBug.CustomFields.TryGetValue(kvp.Key, out var originalVal) || !DeepEquals(originalVal, kvp.Value))
                    {
                        diffs[kvp.Key] = kvp.Value;
                    }
                }
            }

            return diffs;
            
            bool DeepEquals(object a, object b)
            {
                if (a == null && b == null) return true;
                if (a == null || b == null) return false;

                // Fallback: serialize both and compare the strings
                var aJson = JsonConvert.SerializeObject(a);
                var bJson = JsonConvert.SerializeObject(b);
                return aJson == bJson;
            }
        }

        private static bool AreListsEqual<T>(List<T> a, List<T> b)
        {
            if (a == null && b == null) return true;
            if (a == null || b == null) return false;
            if (a.Count != b.Count) return false;
            return a.SequenceEqual(b);
        }
    }
}