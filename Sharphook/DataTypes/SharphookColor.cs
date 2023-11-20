namespace Sharphook.DataTypes
{
    public class SharphookColor
    {
        public uint Value { get; private set; }

        public SharphookColor(uint value = 0)
        {
            Value = value;
        }

        public SharphookColor(byte red, byte green, byte blue)
        {
            Value = Convert.ToUInt32((red << 16) + (green << 8) + blue);
        }

        public SharphookColor()
        {
            Random random = new Random();
            int maxValue = 0xFFFFFF;

            Value = Convert.ToUInt32(random.Next(Convert.ToInt32(maxValue + 1)));
        }

        public void SetValue(uint value)
        {
            Value = value;
        }

        public (byte, byte, byte) ToRGB()
        {
            byte red =  (byte)((Value >> 16) & 0xFF);
            byte green = (byte)((Value >> 8) & 0xFF);
            byte blue = (byte)((Value >> 0) & 0xFF);

            return (red, green, blue);
        }
    }
}
