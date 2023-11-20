using Sharphook.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
