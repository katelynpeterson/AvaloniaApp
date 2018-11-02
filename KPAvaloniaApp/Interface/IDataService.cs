using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataService
    {
        bool FileExists(string imageFile);
        Task<string> FindFileAsync();
    }
}
