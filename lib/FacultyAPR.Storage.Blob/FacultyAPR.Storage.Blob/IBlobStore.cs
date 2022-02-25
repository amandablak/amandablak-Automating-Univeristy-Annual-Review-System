using System;
using System.Threading.Tasks;
using System.IO;

namespace FacultyAPR.Storage.Blob
{
    public interface IBlobStore
    {
        Task<Stream> ReadBlob(string blobName);

        Task<bool> WriteBlob(string blobName, Stream blobStream);

        string GenerateBlobName();
    }
}
