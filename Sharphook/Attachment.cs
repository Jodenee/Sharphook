using Sharphook.ResponseObjects;

namespace Sharphook;

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

	internal Attachment(AttachmentObject attachmentObject)
	{
		Id = Convert.ToUInt64(attachmentObject.Id);
		FileName = attachmentObject.Filename;
		Description = attachmentObject.Description;
		ContentType = attachmentObject.ContentType;
		Size = attachmentObject.Size;
		Url = attachmentObject.Url;
		ProxyUrl = attachmentObject.ProxyUrl;
		Height = attachmentObject.Height;
		Width = attachmentObject.Width;
	}
}
