using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FacultyAPR.Models.CV
{
    public class CVFile : ICVFile
    {
        private Stream _stream;

        public CVFile(
            Stream file,
            string fileName,
            DateTimeOffset formYear,
            Guid facultyId)
        {
            _stream = file ?? throw new ArgumentNullException(nameof(file));
            if (fileName == default || fileName == String.Empty)
            {
                throw new ArgumentNullException(nameof(fileName));
            }
            
            FileExtension = Path.GetExtension(fileName).ToLowerInvariant();
            FileName = Guid.NewGuid().ToString() + FileExtension;

            var allowedTypes = (IEnumerable<string>)Enum.GetNames(typeof(AllowedCVFileTypes));
            var fileInAllowedTypes = allowedTypes
                .Select(s => s.Replace(s, '.' + s))
                .Contains(this.FileExtension, StringComparer.InvariantCultureIgnoreCase);

            if (!fileInAllowedTypes)
            {
                throw new ArgumentOutOfRangeException($"File type .{this.FileExtension} not allowed");
            }
            
            if (facultyId == default)
            {
                throw new ArgumentNullException(nameof(facultyId));
            }
            FacultyId = facultyId;
            if (formYear == default)
            {
                throw new ArgumentNullException(nameof(formYear));
            }
            TimeCreated = formYear;
        }
        public string FileName { get; }

        public DateTimeOffset TimeCreated { get; }

        public Guid FacultyId { get; }

        public string FileExtension { get; }

        public Stream GetStream()
        {
            return _stream;
        }
    }
}