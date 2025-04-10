using System.Collections.Generic;

namespace BugSharp.Remote
{
    /// <summary>
    /// The first response of a attachment call
    /// </summary>
    public struct AttachmentResponse
    {
        /// <summary>
        /// A list of attachments connected to a bug
        /// </summary>
        public Dictionary<int, RemoteAttachment> Attachments { get; set; }
    }
}