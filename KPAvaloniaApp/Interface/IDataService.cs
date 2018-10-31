using System;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IDataService
    {
        bool FileExists(string gedcomFile);
        Task<string> FindFileAsync();
    }
}
