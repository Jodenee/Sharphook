using Sharphook.Models.Bases;
using Sharphook.Utility.Enums;

namespace Sharphook.Models;

public class MessageFlags : BaseFlags
{
	public MessageFlags(int value = 0) : base(value) { }

	public MessageFlags(List<MessageFlag> flags)
		: base()
	{
		foreach (MessageFlag flag in flags)
			SetFlag((int)flag, true);
	}

	public MessageFlags(MessageFlag[] flags)
		: base()
	{
		foreach (MessageFlag flag in flags)
			SetFlag((int)flag, true);
	}

	public bool HasFlags(List<MessageFlag> flags)
	{
		foreach (MessageFlag flag in flags)
			if (!HasFlag((int)flag)) { return false; }

		return true;
	}
	public bool HasFlags(MessageFlag[] flags)
	{
		foreach (MessageFlag flag in flags)
			if (!HasFlag((int)flag)) { return false; }

		return true;
	}

	public List<MessageFlag> GetFlags()
	{
		List<MessageFlag> flags = new List<MessageFlag>();

		foreach (MessageFlag flag in Enum.GetValues(typeof(MessageFlag)))
		{
			if (!HasFlag((int)flag)) { continue; }

			flags.Add(flag);
		}

		return flags;
	}
}
