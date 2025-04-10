using System.Collections.Generic;
using System.Threading.Tasks;
using BugSharp.Exceptions;
using BugSharp.Remote;
using Newtonsoft.Json;

namespace BugSharp.Services
{
    internal class AttachmentService : BaseRequestClient, IAttachmentService
    {
        private readonly BugZilla _bugZilla;
        
        public AttachmentService(BugZilla bugZilla) : base(bugZilla.Settings)
        {
            _bugZilla = bugZilla;
        }

        public async Task<Attachment> GetAttachmentAsync(int attachmentId)
        {
            var jsonResult = await GetAsync(Endpoints.Attachment, attachmentId, _bugZilla.Settings.ApiKey);
            var response = JsonConvert.DeserializeObject<AttachmentResponse>(jsonResult);

            if (!response.Attachments.TryGetValue(attachmentId, out var attachment))
                throw new BugZillaRequestException("Attachment with id " + attachmentId + " not found");
            
            var returnedAttachment = new Attachment(_bugZilla, attachment);
            return returnedAttachment;
        }

        public async Task UpdateAttachmentAsync(Attachment attachment)
        {
            var json = JsonConvert.SerializeObject(attachment);
            await PutAsync(Endpoints.Attachment, attachment.Id, _bugZilla.Settings.ApiKey, json);
        }

        public async Task<int> CreateAttachmentAsync(Attachment attachment)
        {
            var json = JsonConvert.SerializeObject(attachment);
            var response = await PostAsync(Endpoints.Attachment, json, _bugZilla.Settings.ApiKey);

            var dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(response);
            if (dict.TryGetValue("id", out var idObj))
            {
                if (int.TryParse(idObj.ToString(), out var id))
                    return id;
            }

            return -1;
        }
    }
}