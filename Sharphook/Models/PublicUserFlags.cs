using Sharphook.Models.Bases;
using Sharphook.Utility.Enums;

namespace Sharphook.Models;

public class PublicUserFlags : BaseFlags
{
    public PublicUserFlags(int value = 0) : base(value) { }

    public bool HasFlags(List<PublicUserFlag> flags)
    {
        foreach (PublicUserFlag flag in flags)
        {
            if (!HasFlag((int) flag)) { return false; }
        }

        return true;
    }
    public bool HasFlags(PublicUserFlag[] flags)
    {
        foreach (PublicUserFlag flag in flags)
        {
            if (!HasFlag((int) flag)) { return false; }
        }

        return true;
    }

    public List<PublicUserFlag> GetFlags()
    {
        List<PublicUserFlag> flags = new List<PublicUserFlag>();

        foreach (PublicUserFlag flag in Enum.GetValues(typeof(PublicUserFlag)))
        {
            if (!HasFlag((int) flag)) 
                continue;

            flags.Add(flag);
        }

        return flags;
    }
}
