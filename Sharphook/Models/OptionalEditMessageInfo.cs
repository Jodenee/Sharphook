using Sharphook.DataTypes;

namespace Sharphook.Models
{
    public class OptionalEditMessageInfo
    {
        public List<SharphookFile>? Files { get; set; }

        public OptionalEditMessageInfo(
            List<SharphookFile>? files = null
        ) 
        {
            Files = files;
        }
    }
}
