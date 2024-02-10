using Sharphook.Models.ResponseObjects;

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
        public bool IsTTS { get; private set; }
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
            Id = Convert.ToUInt64(messageObject.Id);
            ChannelId = Convert.ToUInt64(messageObject.ChannelId);
            Author = new PartialUser(client, messageObject.Author);
            Content = messageObject.Content;
            CreatedAt = DateTime.Parse(messageObject.CreatedAt);
            Type = messageObject.Type;
            Embeds = new List<Embed>();
            Pinned = messageObject.Pinned;
            MentionEveryone = messageObject.MentionEveryone;
            IsTTS = messageObject.IsTTS;
            Attachments = new List<Attachment>();
            Flags = messageObject.Flags;
            WebhookId = Convert.ToUInt64(messageObject.WebhookId);

            foreach (EmbedObject embedObject in messageObject.Embeds)
            {
                Embeds.Add(embedObject.ToEmbed());
            }

            foreach (AttachmentObject attachmentObject in messageObject.Attachments)
            {
                Attachments.Add(new Attachment(attachmentObject));
            }
        }
    }
}
