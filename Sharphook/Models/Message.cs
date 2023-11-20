using Sharphook.Models.Partials;
using Sharphook.Models.ResponseObjects;
using System;
using System.Collections.Generic;

namespace Sharphook.Models
{
    public class Message
    {
        private WebhookClient Client { get; }
        public UInt64 Id { get; private set; }
        public UInt64 ChannelId { get; private set; }
        public PartialUser Author { get; private set; }
        public string Content { get; private set; }
        public DateTime Timestamp { get; private set; }
        public byte Type { get; private set; }
        public List<Embed> Embeds { get; private set; }
        public bool Pinned { get; private set; }
        public bool MentionEveryone { get; private set; }
        public bool TTS { get; private set; }
        public int Flags { get; private set; }
        public UInt64 WebhookId { get; private set; }

        public Message(WebhookClient client, MessageObject messageObject)
        {
            Client = client;
            Id = Convert.ToUInt64(messageObject.id);
            ChannelId = Convert.ToUInt64(messageObject.channel_id);
            Author = new PartialUser(client, messageObject.author);
            Content = messageObject.content;
            Timestamp = DateTime.Parse(messageObject.timestamp);
            Type = messageObject.type;
            Embeds = new List<Embed>();
            Pinned = messageObject.pinned;
            MentionEveryone = messageObject.mention_everyone;
            TTS = messageObject.tts;
            Flags = messageObject.flags;
            WebhookId = Convert.ToUInt64(messageObject.webhook_id);

            foreach (EmbedObject embedObject in messageObject.embeds)
            {
                Embeds.Add(new Embed(embedObject));
            }
        }
    }
}
