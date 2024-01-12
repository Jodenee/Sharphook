using Sharphook.DataTypes;
using Sharphook.Models.ResponseObjects;
using System.Dynamic;

namespace Sharphook.Models
{
    public class Message
    {
        WebhookClient Client;
        public ulong Id { get; private set; }
        public ulong ChannelId { get; private set; }
        public PartialUser Author { get; private set; }
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public byte Type { get; private set; }
        public List<Embed> Embeds { get; private set; }
        public bool Pinned { get; private set; }
        public bool MentionEveryone { get; private set; }
        public bool TTS { get; private set; }
        public List<Attachment> Attachments { get; private set; }
        public int Flags { get; private set; }
        public ulong WebhookId { get; private set; }
        
        public string JumpUrl 
        {
            get => $"https://discord.com/channels/@me/{ChannelId}/{Id}";
        }

        public Message(WebhookClient client, MessageObject messageObject)
        {
            Client = client;
            Id = Convert.ToUInt64(messageObject.id);
            ChannelId = Convert.ToUInt64(messageObject.channel_id);
            Author = new PartialUser(client, messageObject.author);
            Content = messageObject.content;
            CreatedAt = DateTime.Parse(messageObject.timestamp);
            Type = messageObject.type;
            Embeds = new List<Embed>();
            Pinned = messageObject.pinned;
            MentionEveryone = messageObject.mention_everyone;
            TTS = messageObject.tts;
            Attachments = new List<Attachment>();
            Flags = messageObject.flags;
            WebhookId = Convert.ToUInt64(messageObject.webhook_id);

            foreach (EmbedObject embedObject in messageObject.embeds)
            {
                Embeds.Add(embedObject._ToEmbed());
            }

            foreach (AttachmentObject attachmentObject in messageObject.attachments)
            {
                Attachments.Add(new Attachment(attachmentObject));
            }
        }
    }
}
