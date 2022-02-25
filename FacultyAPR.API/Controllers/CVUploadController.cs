using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.IO;
using FacultyAPR.Models;
using FacultyAPR.Models.CV;
using FacultyAPR.Storage.Blob;

namespace FacultyAPR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CVUploadController : ControllerBase
    {
        private readonly IBlobStore _blobStore;
        public CVUploadController(IBlobStore blobStore)
        {
            _blobStore = blobStore ?? throw new ArgumentNullException(nameof(blobStore));
        }

        //TODO Virus scan?
        [HttpPost]
        [Route("{formYear}/{facultyId}/{formId}")]
        public async Task UploadCV(
            [FromRoute] int formYear,
            [FromRoute] Guid facultyId,
            [FromRoute] Guid formId,
            [FromForm] IFormFile cv,
            CancellationToken ct = default)
        {
            CVFile file;
            try
            {
                file  = new CVFile(cv.OpenReadStream(), cv.FileName, DateTimeOffset.Now, facultyId);
            }
            catch (ArgumentOutOfRangeException)
            {
                HttpContext.Response.StatusCode = StatusCodes.Status415UnsupportedMediaType;
                return;
            }
            var name = _blobStore.GenerateBlobName();
            //save name and details to sql
            //upload data to blob
            await _blobStore.WriteBlob(name, file.GetStream());
        }

        [HttpGet]
        [Route("{formYear}/{facultyId}/{formId}")]
        public async Task<IActionResult> GetCV(
            [FromRoute] int formYear,
            [FromRoute] Guid facultyId,
            [FromRoute] Guid formId,
            CancellationToken ct = default)
        {
            // get faculty info
            // look up file name in sqldb
            // get file from blob
            var blobName = "";
            var blob = await _blobStore.ReadToArrayAsync(blobName);
        
            string mimeType = "application/pdf";
            FileStream SourceStream = new FileStream("FILEPATHHERE", FileMode.Open, FileAccess.Read);
            return File(blob, mimeType, fileDownloadName: "DYNMAICNAMEHERE.pdf");
            
        }
    }
}