using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models;

public class Message
{
    WebhookClient Client;
    public ulong Id { get; private set; }
    public ulong ChannelId { get; private set; }
    public User Author { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public byte Type { get; private set; }
    public List<Embed> Embeds { get; private set; }
    public bool Pinned { get; private set; }
    public bool MentionsEveryone { get; private set; }
    public List<User> MentionedUsers { get; private set; }
    public List<ulong> MentionedRoles { get; private set; }
    public bool IsTTS { get; private set; }
    public List<Attachment> Attachments { get; private set; }
    public MessageFlags Flags { get; private set; }
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
        Author = new User(client, messageObject.Author);
        Content = messageObject.Content;
        CreatedAt = DateTime.Parse(messageObject.CreatedAt);
        Type = messageObject.Type;
        Embeds = new List<Embed>();
        Pinned = messageObject.Pinned;
        MentionsEveryone = messageObject.MentionsEveryone;
        MentionedUsers = new List<User>();
        MentionedRoles = new List<ulong>();
        IsTTS = messageObject.IsTTS;
        Attachments = new List<Attachment>();
        Flags = new MessageFlags(messageObject.Flags);
        WebhookId = Convert.ToUInt64(messageObject.WebhookId);

        foreach (EmbedObject embedObject in messageObject.Embeds)
        {
            Embeds.Add(embedObject.ToEmbed());
        }

        foreach (AttachmentObject attachmentObject in messageObject.Attachments)
        {
            Attachments.Add(new Attachment(attachmentObject));
        }

        foreach (UserObject userObject in messageObject.MentionedUsers)
        {
            MentionedUsers.Add(new User(Client, userObject));
        }

        foreach (string mentionedRole in messageObject.MentionedRoles)
        {
            MentionedRoles.Add(Convert.ToUInt64(mentionedRole));
        }
    }
}
