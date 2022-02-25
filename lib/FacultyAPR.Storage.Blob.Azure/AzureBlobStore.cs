using System;
using System.Threading.Tasks;
using System.IO;

namespace FacultyAPR.Storage.Blob.Azure
{
    public class AzureBlobStore : IBlobStore
    {
        public string GenerateBlobName()
        {
            throw new NotImplementedException();
        }

        public Task<Stream> ReadBlob(string blobName)
        {
            throw new NotImplementedException();
        }

        public Task<bool> WriteBlob(string blobName, Stream blobStream)
        {
            throw new NotImplementedException();
        }
    }
}
