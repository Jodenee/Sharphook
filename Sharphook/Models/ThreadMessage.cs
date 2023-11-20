using Sharphook.Models.ResponseObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharphook.Models
{
    public class ThreadMessage : Message
    {
        public uint Position { get; private set; }

        public ThreadMessage(WebhookClient client, ThreadMessageObject threadMessageObject) : base(client, threadMessageObject)
        {
            Position = threadMessageObject.position;
        }
    }
}
