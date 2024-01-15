namespace Sharphook.DataTypes
{
    public class SharphookFile
    {
        private string? _filename;

        public FileStream Stream { get; private set; }
        public bool Spoiler { get; private set; }
        public string? Description { get; private set; }
        public string FileName
        {
            get {
                if (Spoiler)
                {
                    return "SPOILER_" + (_filename ?? Path.GetFileName(Stream.Name));
                }
                else
                {
                    return _filename ?? Path.GetFileName(Stream.Name);
                }
            }
        }
        public string AttachmentLink {
            get => $"attachment://{FileName}";
        }

        public SharphookFile(string filepath, string? filename = null, bool spoiler = false, string? description = null) 
        {
            _filename = filename;

            Stream = File.OpenRead(filepath);
            Spoiler = spoiler;
            Description = description;
        }

        public SharphookFile(FileStream fileStream, string? filename = null, bool spoiler = false, string? description = null)
        {
            _filename = filename;

            Stream = fileStream;
            Spoiler = spoiler;
            Description = description;
        }

        internal object _ToObject(int index)
        {
            return new
            {
                id = index,
                filename = FileName,
                description = Description,
            };
        }

        public async Task DisposeAsync()
        {
            await Stream.DisposeAsync();
        }
    }
}
