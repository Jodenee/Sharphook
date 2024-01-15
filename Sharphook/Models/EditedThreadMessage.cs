using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
    public class EditedThreadMessage : ThreadMessage
    {
        public DateTime EditedAt { get; private set; }

        public EditedThreadMessage(WebhookClient client, EditedThreadMessageObject editedThreadMessageObject) 
            : base(client, editedThreadMessageObject)
        {
            EditedAt = DateTime.Parse(editedThreadMessageObject.edited_timestamp);
        }
    }
}