#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable IDE1006 // Naming Styles

namespace Sharphook.Models.ResponseObjects.PartialObjects
{
    public class PartialUserObject
    {
        public string id { get; set; }
        public string username { get; set; }
        public string? avatar { get; set; }
        public string discriminator { get; set; }
        public int public_flags { get; set; }
        public int flags { get; set; }
        public bool? bot { get; set; }
    }
}
