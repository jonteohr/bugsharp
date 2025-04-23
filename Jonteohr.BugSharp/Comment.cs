using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp
{
    /// <summary>
    /// A BugZilla comment
    /// </summary>
    public class Comment
    {
        private readonly BugZilla _bugZilla;
        private RemoteComment _originalComment;
        
        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <param name="bugZilla">BugZilla instance that owns this comment</param>
        public Comment(BugZilla bugZilla) : this(bugZilla, new RemoteComment())
        {
        }
        
        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <param name="bugzilla">BugZilla instance that owns this comment</param>
        /// <param name="remoteComment">The remote comment object</param>
        public Comment(BugZilla bugzilla, RemoteComment remoteComment)
        {
            _bugZilla = bugzilla;
            Initialize(remoteComment);
        }

        private void Initialize(RemoteComment remoteComment)
        {
            _originalComment = remoteComment;
            
            Time = remoteComment.time;
            Text = remoteComment.text;
            BugId = remoteComment.bug_id;
            Count = remoteComment.count;
            AttachmentId = remoteComment.attachment_id ?? -1;
            IsPrivate = remoteComment.is_private;
            Tags = remoteComment.tags != null ? remoteComment.tags.ToList() : new List<string>();
            Creator = remoteComment.creator;
            CreationTime = remoteComment.creation_time;
            Id = remoteComment.id;
        }
        
        /// <summary>
        /// The time (in Bugzilla’s timezone) that the comment was added.
        /// </summary>
        public DateTime Time { get;set; }
        
        /// <summary>
        /// The actual text of the comment.
        /// </summary>
        [JsonProperty("comment")]
        public string Text { get; set; }
        
        /// <summary>
        /// The ID of the bug that this comment is on.
        /// </summary>
        public int BugId { get; set; }
        
        /// <summary>
        /// The number of the comment local to the bug. The Description is 0, comments start with 1.
        /// </summary>
        public int Count { get; set; }
        
        /// <summary>
        /// If the comment was made on an attachment, this will be the ID of that attachment. Otherwise it will be null.
        /// </summary>
        public int AttachmentId { get; set; }
        
        /// <summary>
        /// true if this comment is private (only visible to a certain group called the “insidergroup”), false otherwise.
        /// </summary>
        public bool IsPrivate { get; set; }
        
        /// <summary>
        /// A list of tags associated with the comment
        /// </summary>
        public List<string> Tags { get; set; }
        
        /// <summary>
        /// The login name of the comment’s author.
        /// </summary>
        public string Creator { get; set; }
        
        /// <summary>
        /// This is exactly same as the time key. Use this field instead of time for consistency with other methods including Get Bug and Get Attachment.<br />
        /// For compatibility, time is still usable. However, please note that time may be deprecated and removed in a future release.
        /// </summary>
        public DateTime CreationTime { get; set; }
        
        /// <summary>
        /// The globally unique ID for the comment.
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Saves a comment on the remote BugZilla server.
        /// </summary>
        /// <exception cref="BugZillaRequestException">Thrown if a comment with that ID already exists. This property must be unset to properly function.</exception>
        public async Task<Comment> SaveChangesAsync()
        {
            if (!string.IsNullOrEmpty(_originalComment.id.ToString()))
                throw new BugZillaRequestException("The comment with id " + Id + " already exists on the server.");
            
            var newId = await _bugZilla.Comments.CreateCommentAsync(this);
            var comments = await _bugZilla.Comments.GetCommentsAsync(BugId);
            var serverComment = comments.FirstOrDefault(k => k.Id == newId);
            
            return serverComment;
        }
    }
}