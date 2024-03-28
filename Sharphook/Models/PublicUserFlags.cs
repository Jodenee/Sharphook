using Sharphook.Models.Bases;
using Sharphook.Utility.Enums;

namespace Sharphook.Models;

public class PublicUserFlags : BaseFlags
{
    public PublicUserFlags(int value = 0) : base(value)
    { }

    public bool HasFlags(List<UserPublicFlag> flags)
    {
        foreach (UserPublicFlag flag in flags)
        {
            if (!HasFlag((int)flag)) { return false; }
        }

        return true;
    }
    public bool HasFlags(UserPublicFlag[] flags)
    {
        foreach (UserPublicFlag flag in flags)
        {
            if (!HasFlag((int)flag)) { return false; }
        }

        return true;
    }

    public List<UserPublicFlag> GetFlags()
    {
        List<UserPublicFlag> flags = new List<UserPublicFlag>();

        foreach (UserPublicFlag flag in Enum.GetValues(typeof(UserPublicFlag)))
        {
            if (!HasFlag((int)flag)) { continue; }

            flags.Add(flag);
        }

        return flags;
    }
}
