using Sharphook.DataTypes;

namespace Sharphook.Models
{
    public class OptionalEditMessageInfo
    {
        public List<SharphookFile> Files { get; set; }

        public OptionalEditMessageInfo(
            List<SharphookFile>? files = null
        ) 
        {
            Files = files ?? new List<SharphookFile>();
        }

        public async Task DisposeAsync()
        {
            foreach (SharphookFile file in Files)
            {
                await file.DisposeAsync();
            }
        }
    }
}
