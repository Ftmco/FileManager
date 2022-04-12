using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Abstraction;

public interface IFileGet : IAsyncDisposable
{
    Task<GetFileResponse> GetFileBase64Async(string token);

    Task<string> CreateDataUrlAsync(string base64,string mime);
}
