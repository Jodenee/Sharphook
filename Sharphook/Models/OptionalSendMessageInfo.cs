using Sharphook.DataTypes;

namespace Sharphook.Models
{
    public class OptionalSendMessageInfo
    {
        public bool? TTS { get; set; }
        public string? UsernameOverride { get; set; }
        public string? AvatarUrlOverride { get; set; }
        public uint? MessageFlags { get; set; }
        public string? ThreadName { get; set; }
        public List<SharphookFile> Files { get; set; }

        public OptionalSendMessageInfo(
            bool? tts = null, 
            string? usernameOverride = null, 
            string? avatarUrlOverride = null, 
            uint? messageFlags = null, 
            string? threadName = null, 
            List<SharphookFile>? files = null
        ) 
        { 
            TTS = tts;
            UsernameOverride = usernameOverride;
            AvatarUrlOverride = avatarUrlOverride;
            MessageFlags = messageFlags;
            ThreadName = threadName;
            Files = files ?? new List<SharphookFile>();
        }

        public async Task DisposeAsync()
        {
            foreach (SharphookFile file in Files)
            {
                await file.DisposeAsync();
            }
        }
    }
}
