using System.IO;

namespace FacultyAPR.Models.CV
{
    public interface IStreamableFile
    {
        Stream GetStream();
    }
}