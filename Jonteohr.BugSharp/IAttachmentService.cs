using System.Threading.Tasks;

namespace BugSharp
{
    /// <summary>
    /// The attachment handling service
    /// </summary>
    public interface IAttachmentService
    {
        /// <summary>
        /// Gets a attachment of the specified ID
        /// </summary>
        /// <param name="attachmentId">The id of the attachment.</param>
        Task<Attachment> GetAttachmentAsync(int attachmentId);
        
        /// <summary>
        /// Update a attachment and save it on the remove server
        /// </summary>
        /// <param name="attachment">The modified attachment object</param>
        Task UpdateAttachmentAsync(Attachment attachment);
        
        /// <summary>
        /// Create a new attachment to the remote server.
        /// </summary>
        /// <param name="attachment">Attachment object to send to server</param>
        /// <returns>Attachment ID if successful, otherwise -1</returns>
        Task<int> CreateAttachmentAsync(Attachment attachment);
    }
}