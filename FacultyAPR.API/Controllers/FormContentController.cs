using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FacultyAPR.Models.Form;
using System;
using System.Linq;
using System.Threading.Tasks;
using FacultyAPR.Models;
using FacultyAPR.Storage;

namespace FacultyAPR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormContentController : ControllerBase
    {
        
        public FormContentController(IFormContentStore formStore, IUserStore userStore)
        {
            this._formStore = formStore ?? throw new ArgumentNullException(nameof(formStore));
            this._userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));

        }

        [HttpGet]
        [Route("{facultyId}/{formId}")]
        public async Task<IActionResult> GetFormContentById(
            [FromRoute] Guid facultyId,
            [FromRoute] Guid formId)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await _userStore.Get(identity);
            if (!users.Any())
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await _formStore.Get(facultyId, formId));
        }


        // Note there might be write conflicts if hit concurrently here
        [HttpPatch]
        [Route("{facultyId}/{formId}")]
        public async Task<IActionResult> UpdateFormById(
            [FromRoute] Guid facultyId,
            [FromRoute] Guid formId,
            [FromBody] FormContent updatedForm)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await _userStore.Get(identity);
            if (!users.Any())
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await _formStore.Update(facultyId, formId, updatedForm));
        }

        //Note formId will be ignored if set
        // Spot scores need to be loaded in separately.
        [HttpPost]
        [Route("{facultyId}")]
        public async Task<IActionResult> CreateFormContent(
            [FromRoute] Guid facultyId,
            [FromBody] FormContent formData)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await _userStore.Get(identity);
            if (!users.Any())
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await _formStore.Create(facultyId, formData));
        }

        [HttpPost]
        [Route("scores/{facultyId}/{formId}")]
        public async Task<IActionResult> CreateFormSpotScoreContent(
            [FromRoute] Guid formId,
            [FromRoute] Guid facultyId,
            [FromBody] IEnumerable<SpotScore> scores)
        {
            await _formStore.Create(facultyId, formId, scores);
            return Ok();
        }

        [HttpDelete]
        [Route("{facultyId}/{formId}")]
        public async Task<IActionResult> RemoveFormContentById(
            [FromRoute] Guid facultyId,
            [FromRoute] Guid formId)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await _userStore.Get(identity);
            if (!users.Any())
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await _formStore.Delete(facultyId, formId));
        }

        [HttpGet]
        [Route("{facultyId}/all")]
        public async Task<ActionResult<IEnumerable<FormStub>>> GetAllFormStubs(
            [FromRoute] Guid facultyId)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await _userStore.Get(identity);
            if (!users.Any())
            {
                return Unauthorized("User does not have access.");
            }
            
            IEnumerable<FormStub> results;
            try
            {
                results =  await _formStore.GetAll(facultyId);
            }
            catch (Exception)
            {
                results = new List<FormStub>();
            }
            return Ok(results);
        }

        private IFormContentStore _formStore;
        private IUserStore _userStore;
    }
}