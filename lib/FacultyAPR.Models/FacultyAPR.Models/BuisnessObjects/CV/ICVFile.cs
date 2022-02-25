using System;
using System.Collections.Generic;
using System.IO;

namespace FacultyAPR.Models.CV
{
    public interface ICVFile : IStreamableFile
    {
        string FileName { get; }

        string FileExtension { get; }

        DateTimeOffset TimeCreated { get; }

        Guid FacultyId { get; }
    }
}