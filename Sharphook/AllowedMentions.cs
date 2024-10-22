using Sharphook.ResponseObjects;
using Sharphook.Utility.Enums;

namespace Sharphook;

public class AllowedMentions
{
	public AllowedMentionType? AllowedTypes;
	public List<ulong> AllowedRoleMentions = new List<ulong>();
	public List<ulong> AllowedUserMentions = new List<ulong>();

	public static AllowedMentions All => new AllowedMentions(
		AllowedMentionType.Everyone | AllowedMentionType.Roles | AllowedMentionType.Users);

	public static AllowedMentions None => new AllowedMentions();

	public AllowedMentions(AllowedMentionType? allowedTypes = null)
	{
		AllowedTypes = allowedTypes;
	}

	internal AllowedMentionsObject ToAllowedMentionsObject()
	{
		AllowedMentionsObject allowedMentionsObject = new AllowedMentionsObject();
		allowedMentionsObject.Parse = new List<string>();

		if ((AllowedTypes & AllowedMentionType.Everyone) == AllowedMentionType.Everyone)
			allowedMentionsObject.Parse.Add("everyone");

		if ((AllowedTypes & AllowedMentionType.Roles) == AllowedMentionType.Roles)
			allowedMentionsObject.Parse.Add("roles");
		else
			allowedMentionsObject.Roles = AllowedRoleMentions.ToArray();

		if ((AllowedTypes & AllowedMentionType.Users) == AllowedMentionType.Users)
			allowedMentionsObject.Parse.Add("users");
		else
			allowedMentionsObject.Users = AllowedUserMentions.ToArray();

		return allowedMentionsObject;
	}
}
