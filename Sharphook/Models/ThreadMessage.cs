using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models;

public class ThreadMessage : Message
{
    public uint Position { get; private set; }

    public ThreadMessage(WebhookClient client, ThreadMessageObject threadMessageObject)
        : base(client, threadMessageObject)
    {
        Position = threadMessageObject.Position;
    }
}
