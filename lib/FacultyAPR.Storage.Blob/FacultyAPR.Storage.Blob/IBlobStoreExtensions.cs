using System.IO;
using System.Threading.Tasks;

namespace FacultyAPR.Storage.Blob
{
    public static class IBlobStoreExtensions
    {
        public static async Task<byte[]> ReadToArrayAsync(this IBlobStore store, string blobName)
        {
            using(var memoryStream = new MemoryStream())
            {
                var stream = await store.ReadBlob(blobName);
                await stream.CopyToAsync(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin); 
                return memoryStream.ToArray();
            }
        }
    }
}