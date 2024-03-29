namespace Sharphook.Models.Bases;

public class BaseFlags
{
    public int Value { get; private set; }

    public BaseFlags(int value = 0)
    {
        Value = value;
    }

    protected bool HasFlag(int flag)
    {
        return (Value & flag) == flag;
    }

    protected void SetFlag(int flag, bool toggle)
    {
        if (toggle)
            Value |= flag;
        else
            Value &= ~flag;
    }
}
