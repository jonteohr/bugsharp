using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Remote;

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

            Id = remoteBug.Id;
            Summary = remoteBug.Summary;
            Status = remoteBug.Status;
            Priority = remoteBug.Priority;
            Product = remoteBug.Product;
            Resolution = remoteBug.Resolution;
            QAContact = remoteBug.qa_contact;
            Version = remoteBug.Version;
            Cc = remoteBug.Cc;
            Platform = remoteBug.Platform;
            Classification = remoteBug.Classification;
            IsOpen = remoteBug.is_open;
            AssignedTo = remoteBug.assigned_to;
            Component = remoteBug.Component;
            IsConfirmed = remoteBug.IsConfirmed;
            Created = remoteBug.creation_time;
            Changed = remoteBug.last_change_time;
            Creator = remoteBug.Creator; // TODO Change this to a custom class with more information on the creator
            Severity = remoteBug.Severity;
            TargetMilestone = remoteBug.target_milestone;
            CustomFields = remoteBug.CustomFields;
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
            if (string.IsNullOrEmpty(_originalBug.Id.ToString()))
            {
                var newKey = await _bugZilla.Bugs.CreateBugAsync(this);
                serverBug = await _bugZilla.Bugs.GetBugAsync(newKey);
            }
            else
            {
                await _bugZilla.Bugs.UpdateBugAsync(this);
                serverBug = await _bugZilla.Bugs.GetBugAsync(_originalBug.Id);
            }

            return serverBug;
        }
    }
}