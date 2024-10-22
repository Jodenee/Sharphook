namespace Sharphook;

public sealed class SharphookFile
{
	private readonly string _filename;

	public string FilePath;
	public bool Spoiler { get; private set; }
	public string? Description { get; private set; }
	public string FileName
	{
		get => Spoiler ? $"SPOILER_{_filename}" : _filename;
	}
	public string AttachmentLink
	{
		get => $"attachment://{FileName}";
	}
	public FileStream Stream
	{
		get => File.OpenRead(FilePath);
	}

	public SharphookFile(string filepath, string? filename = null, bool spoiler = false, string? description = null)
	{
		FilePath = filepath;
		Spoiler = spoiler;
		Description = description;

		_filename = filename ?? Path.GetFileName(FilePath);
	}

	public SharphookFile(FileStream fileStream, string? filename = null, bool spoiler = false, string? description = null)
	{
		FilePath = fileStream.Name;
		Spoiler = spoiler;
		Description = description;

		_filename = filename ?? Path.GetFileName(fileStream.Name);
	}

	internal object ToObject(int index)
	{
		return new
		{
			id = index,
			filename = FileName,
			description = Description,
		};
	}
}
