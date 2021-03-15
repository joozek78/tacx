using System.IO;
using System.Threading.Tasks;

namespace Tacx.Activities.Api.Core.Adapters
{
    public interface IFileRepository
    {
        Task Upload(string fileName, byte[] contents);
        Task Delete(string fileName);
    }
}