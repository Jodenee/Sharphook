using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
    public class EditedThreadMessage
    {
        public DateTime EditedTimestamp { get; private set; }

        public EditedThreadMessage(WebhookClient client, EditedThreadMessageObject editedThreadMessageObject) 
        { 
            EditedTimestamp = DateTime.Parse(editedThreadMessageObject.edited_timestamp);
        }
    }
}
