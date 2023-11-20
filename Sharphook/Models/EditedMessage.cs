using Sharphook.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
