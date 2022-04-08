using File.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File.Abstraction;

public interface IDirectoryViewModel : IAsyncDisposable
{
    Task<DirectoryViewModel> CreateDirectoryViewModelAsync(FDirectory directory);

    Task<IEnumerable<DirectoryViewModel>> CreateDirectoryViewModelAsync(IEnumerable<FDirectory> directories);
}
