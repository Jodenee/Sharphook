using Sharphook.ResponseObjects;
using Sharphook.Utility.Enums;
using Sharphook.Utility.Formatters;

namespace Sharphook;

public class User
{
	private readonly WebhookClient _client;
	public ulong Id { get; private set; }
	public string Username { get; private set; }
	public string? GlobalName { get; private set; }
	public Asset? Avatar { get; private set; }
	public PublicUserFlag PublicFlags { get; private set; }
	public bool IsBot { get; private set; }

	public string Mention
	{
		get => MentionFormatter.User(Id);
	}

	internal User(WebhookClient client, UserObject userObject)
	{
		_client = client;
		Id = Convert.ToUInt64(userObject.Id);
		Username = userObject.Username;
		GlobalName = userObject.GlobalName;
		Avatar = userObject.Avatar != null
			? Asset.FromAvatar(_client, Id, userObject.Avatar)
			: null;
		PublicFlags = userObject.PublicFlags;
		IsBot = userObject.IsBot ?? false;
	}
}
