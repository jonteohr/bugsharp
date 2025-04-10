using System;
using System.IO;
using System.Threading.Tasks;
using BugSharp.Remote;

namespace BugSharp
{
    /// <summary>
    /// A bugzilla attachment
    /// </summary>
    public class Attachment
    {
        private readonly BugZilla _bugZilla;
        private RemoteAttachment _originalAttachment;

        /// <summary>
        /// Creates a new attachment
        /// </summary>
        /// <param name="bugZilla">BugZilla instance that owns this bug</param>
        public Attachment(BugZilla bugZilla) : this(bugZilla, new RemoteAttachment())
        {
        }

        /// <summary>
        /// Creates a new attachment
        /// </summary>
        /// <param name="bugZilla">BugZilla instance that owns this bug</param>
        /// <param name="originalAttachment">The remote attachment object</param>
        public Attachment(BugZilla bugZilla, RemoteAttachment originalAttachment)
        {
            _bugZilla = bugZilla;
            Initialize(originalAttachment);
        }

        private void Initialize(RemoteAttachment remoteAttachment)
        {
            _originalAttachment = remoteAttachment;

            LastChanged = remoteAttachment.last_change_time;
            Data = remoteAttachment.data;
            ContentType = remoteAttachment.content_type;
            Size = remoteAttachment.size;
            IsPatch = remoteAttachment.is_patch;
            Id = remoteAttachment.id;
            Summary = remoteAttachment.summary;
            FileName = remoteAttachment.file_name;
            IsObsolete = remoteAttachment.is_obsolete;
            Creator = remoteAttachment.creator;
            IsPrivate = remoteAttachment.is_private;
            BugId = remoteAttachment.bug_id;
            Flags = remoteAttachment.flags;
            Comment = remoteAttachment.comment;
        }
        
        /// <summary>
        /// The last time the attachment was modified.
        /// </summary>
        public DateTime LastChanged { get; set; }
        
        /// <summary>
        /// The raw data of the attachment, encoded as Base64.
        /// </summary>
        public string Data { get; set; }
        
        /// <summary>
        /// The MIME type of the attachment.
        /// </summary>
        public string ContentType { get; set; }
        
        /// <summary>
        /// The length (in bytes) of the attachment.
        /// </summary>
        public int Size { get; set; }
        
        /// <summary>
        /// true if the attachment is a patch, false otherwise.
        /// </summary>
        public bool IsPatch { get; set; }
        
        /// <summary>
        /// The numeric ID of the attachment.
        /// </summary>
        public int Id { get; private set; }
        
        /// <summary>
        /// A short string describing the attachment.
        /// </summary>
        public string Summary { get; set; }
        
        /// <summary>
        /// The file name of the attachment.
        /// </summary>
        public string FileName { get; set; }
        
        /// <summary>
        /// true if the attachment is obsolete, false otherwise.
        /// </summary>
        public bool IsObsolete { get; set; }
        
        /// <summary>
        /// The login name of the user that created the attachment.
        /// </summary>
        public string Creator { get; set; }
        
        /// <summary>
        /// true if the attachment is private (only visible to a certain group called the “insidergroup”, false otherwise.
        /// </summary>
        public bool IsPrivate { get; set; }
        
        /// <summary>
        /// The numeric ID of the bug that the attachment is attached to.
        /// </summary>
        public int BugId { get; set; }
        
        /// <summary>
        /// Array of objects, each containing the information about the flag currently set for each attachment. Each flag object contains items descibed in the Flag object below.
        /// </summary>
        public object[] Flags { get; set; }
        
        /// <summary>
        /// A comment to add along with this attachment.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Saves the field changes to server
        /// </summary>
        /// <returns>The updated attachment from the server</returns>
        public async Task<Attachment> SaveChangesAsync()
        {
            Attachment serverAttachment;
            if (string.IsNullOrEmpty(_originalAttachment.id.ToString()))
            {
                var newId = await _bugZilla.Attachments.CreateAttachmentAsync(this);
                serverAttachment = await _bugZilla.Attachments.GetAttachmentAsync(newId);
            }
            else
            {
                await _bugZilla.Attachments.UpdateAttachmentAsync(this);
                serverAttachment = await _bugZilla.Attachments.GetAttachmentAsync(_originalAttachment.id);
            }

            return serverAttachment;
        }

        /// <summary>
        /// Downloads the attachment to the specified path
        /// </summary>
        /// <param name="path">Filepath to download the attachment to.</param>
        /// <returns>Filepath to download</returns>
        public string Download(string path)
        {
            var fileBytes = Convert.FromBase64String(Data);
            var finalPath = Path.Combine(path, FileName);
            File.WriteAllBytes(finalPath, fileBytes);

            return finalPath;
        }
    }
}