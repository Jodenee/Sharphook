using Sharphook.Models.ResponseObjects.PartialObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharphook.Models.ResponseObjects
{
    public class MessageObject
    {
        public string id { get; set; }
        public string channel_id { get; set; }
        public PartialUserObject author { get; set; }
        public string content { get; set; }
        public string timestamp { get; set; }
        public byte type { get; set; }
        public List<EmbedObject> embeds { get; set; }

        public bool pinned { get; set; }
        public bool mention_everyone { get; set; }
        public bool tts { get; set; }
        public int flags { get; set; }
        public string webhook_id { get; set; }
    }
}
