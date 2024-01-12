using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
    public class EditedMessage : Message
    {
        public DateTime EditedAt { get; private set; }

        public EditedMessage(WebhookClient client, EditedMessageObject editedMessageObject) : base(client, editedMessageObject)
        {
            EditedAt = DateTime.Parse(editedMessageObject.edited_timestamp);
        }
    }
}
