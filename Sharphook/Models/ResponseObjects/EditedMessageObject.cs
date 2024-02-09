#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles

namespace Sharphook.Models.ResponseObjects
{
    public class EditedMessageObject : MessageObject
    {
        public string edited_timestamp { get; set; }
    }
}
