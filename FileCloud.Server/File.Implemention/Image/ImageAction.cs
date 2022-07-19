using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace File.Implemention;

public class ImageAction : IImageAction
{
    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        return ValueTask.CompletedTask;
    }

    public  Task<string> ResizeImageAsync(string path, int width, int height)
    {
        
        throw new NotImplementedException();
    }
}