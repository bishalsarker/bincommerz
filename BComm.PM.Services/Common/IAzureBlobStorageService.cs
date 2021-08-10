using System.IO;
using System.Threading.Tasks;

namespace BComm.PM.Services.Common
{
    public interface IAzureBlobStorageService
    {
        Task DeleteFileAsync(string blobContainer, string fileName);
        Task UploadFileAsync(string blobContainer, string fileName, Stream stream);
    }
}