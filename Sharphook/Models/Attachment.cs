using Sharphook.Models.ResponseObjects;

namespace Sharphook.Models
{
	public class Attachment
	{
		public ulong Id { get; private set; }
        public string FileName { get; private set; }
        public string? Description { get; private set; }
        public string? ContentType { get; private set; }
        public int Size { get; private set; }
        public string Url { get; private set; }
        public string ProxyUrl { get; private set; }
        public int? Height { get; private set; }
		public int? Width { get; private set; }
		public int Flags { get; private set; }

		public float SizeInKiloBytes
		{
			get => Size / 1_000;
		}

        public float SizeInMegaBytes
        {
            get => Size / 1_000_000;
        }

		public bool IsSpoiler
		{
			get => FileName.StartsWith("SPOILER_");
		}

		public Attachment(AttachmentObject attachmentObject) 
		{
			Id = attachmentObject.id;
			FileName = attachmentObject.filename;
			Description = attachmentObject.description;
			ContentType = attachmentObject.content_type;
			Size = attachmentObject.size;
			Url = attachmentObject.url;
			ProxyUrl = attachmentObject.proxy_url;
			Height = attachmentObject.height;
			Width = attachmentObject.width;
			Flags = attachmentObject.flags ?? 0;
        }
	}
}
