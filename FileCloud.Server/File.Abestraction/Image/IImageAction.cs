namespace File.Abstraction;

public interface IImageAction : IAsyncDisposable
{
    Task<string> ResizeImageAsync(string path, int width, int height);
}