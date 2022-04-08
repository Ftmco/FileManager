using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Abstraction;

public interface IFileAction : IAsyncDisposable
{
    Task<CreateFileResponse> CreateFileAsync(CreateFile file);

    Task<bool> SaveFileAsync(SaveFile saveFile);
}
