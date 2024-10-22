namespace Sharphook;

public class SharphookColor
{
	private const uint _maxValue = 0xFFFFFF;
	public uint Value { get; private set; }

	public byte Red
	{
		get => (byte)(Value >> 16 & 0xFF);
	}

	public byte Green
	{
		get => (byte)(Value >> 8 & 0xFF);
	}

	public byte Blue
	{
		get => (byte)(Value >> 0 & 0xFF);
	}

	public SharphookColor(uint value)
	{
		if (value > _maxValue) throw new ArgumentOutOfRangeException($"value needs to be less than or equal to {_maxValue}!");

		Value = value;
	}

	public SharphookColor(byte red, byte green, byte blue)
	{
		Value = Convert.ToUInt32((red << 16) + (green << 8) + blue);
	}
}
