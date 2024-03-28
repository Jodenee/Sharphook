namespace Sharphook.Models;

public class OptionalEditMessageInfo
{
    public List<SharphookFile>? Files { get; set; }

    public OptionalEditMessageInfo(
        List<SharphookFile>? files = null
    )
    {
        Files = files;
    }

    public async Task DisposeAsync()
    {
        if (Files != null)
        {
            foreach (SharphookFile file in Files)
            {
                await file.DisposeAsync();
            }
        }
    }
}
