using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
    public class EditedMessage : Message
    {
        public DateTime EditedTimestamp { get; private set; }

        public EditedMessage(WebhookClient client, EditedMessageObject editedMessageObject) : base(client, editedMessageObject)
        {
            EditedTimestamp = DateTime.Parse(editedMessageObject.edited_timestamp);
        }
    }
}
