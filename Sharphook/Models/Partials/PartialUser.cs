using Sharphook.Models.ResponseObjects.PartialObjects;

namespace Sharphook.Models
{
	public class PartialUser
	{
		WebhookClient Client { get; set; }
		public ulong Id { get; private set; }
		public string Username { get; private set; }
		public string? AvatarHash { get; private set; }
		public int PublicFlags { get; private set; }
		public bool IsBot { get; private set; }

		public PartialUser(WebhookClient client, PartialUserObject partialUserObject)
		{
			Client = client;
			Id = Convert.ToUInt64(partialUserObject.Id);
			Username = partialUserObject.Username;
			AvatarHash = partialUserObject.Avatar;
            IsBot = partialUserObject.IsBot ?? false;
		}

		public string? GetAvatarUrl()
		{
			if (AvatarHash == null) { return null; }

			return $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}.png";
		}

		public string? GetAvatarUrl(int size, string? imageFormat)
		{
			if (AvatarHash == null) { return null; }

			return $"https://cdn.discordapp.com/avatars/{Id}/{AvatarHash}{imageFormat ?? ".png"}?size={size}";
		}

		public bool HasAnimatedAvatar()
		{ 
			if (AvatarHash == null) { return false; }

			return AvatarHash.StartsWith("a_");
		}
	}
}
