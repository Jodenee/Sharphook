namespace Sharphook.Models
{
    public class OptionalSendMessageInfo
    {
        public bool? TTS { get; set; }
        public string? UsernameOverride { get; set; }
        public string? AvatarUrlOverride { get; set; }
        public uint? MessageFlags { get; set; }
        public string? ThreadName { get; set; }

        public OptionalSendMessageInfo(bool? tts = null, string? usernameOverride = null, string? avatarUrlOverride = null, uint? messageFlags = null, string? threadName = null) 
        { 
            TTS = tts;
            UsernameOverride = usernameOverride;
            AvatarUrlOverride = avatarUrlOverride;
            MessageFlags = messageFlags;
            ThreadName = threadName;
        }
    }
}
