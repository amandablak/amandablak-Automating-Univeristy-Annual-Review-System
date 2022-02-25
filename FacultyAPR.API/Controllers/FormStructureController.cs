using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FacultyAPR.Models.Form;
using System;
using System.Threading.Tasks;
using FacultyAPR.Storage;
using FacultyAPR.Models;
using System.Linq;

namespace FacultyAPR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormStructureController : ControllerBase
    {
        public FormStructureController(IFormStructureStore formStore, IUserStore userStore)
        {
            this._formStore = formStore ?? throw new ArgumentNullException(nameof(formStore));

            this._userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));
        }

        //TODO AUTH to validate user is who they say they are
        [HttpGet]
        [Route("{formId}")]
        public async Task<IActionResult> GetForm(
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
            
            return Ok(await _formStore.Get(formId));
        }

        [HttpGet]
        [Route("{formYear}/{formRank}")]
        public async Task<IActionResult> GetForm(
            [FromRoute] string formYear,
            [FromRoute] FacultyRank formRank)
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
            
            return Ok(await _formStore.Get(formYear, formRank));
        }

        // Note there might be write conflicts if hit concurrently here
        [HttpPatch]
        [Route("{formId}")]
        public async Task<IActionResult> UpdateFormById(
            [FromRoute] Guid formId,
            [FromBody] FormStructure updatedForm)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }

            var users = await _userStore.Get(identity);
            if (users.All(u => u.UserType != UserType.Admin))
            {
                return Unauthorized("User does not have access.");
            }
            
            if(updatedForm is null) return BadRequest("Form cannot be null");
            if(updatedForm?.Groups is null) return BadRequest("Form Groups cannot be null");
            if(updatedForm?.FormYear is null) return BadRequest("Form Year cannot be null");
            updatedForm.FormId = formId;
            return Ok(await _formStore.Update(updatedForm));
        }

        //Note formId will be ignored if set
        [HttpPost]
        [Route("{formYear}/{formRank}")]
        public async Task<IActionResult> CreateForm(
            [FromRoute] string formYear,
            [FromRoute] FacultyRank formRank,
            [FromBody] FormStructure structure)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }

            var users = await _userStore.Get(identity);
            if (users.All(u => u.UserType != UserType.Admin))
            {
                return Unauthorized("User does not have access.");
            }
            
            structure.FormId = Guid.NewGuid();
            var updatedGroups = structure.Groups.Select(g => 
            {
                var group = new Group();
                group.GroupId = Guid.NewGuid();;
                group.Title = g.Title;
                group.Description = g.Description;
                group.Sections = g.Sections.Select(s => {
                    var section = new Section();
                    section.GroupId = group.GroupId;
                    section.SectionId = Guid.NewGuid();
                    section.SectionTitle = s.SectionTitle;
                    section.SectionDescription = s.SectionDescription;
                    section.SectionType = s.SectionType;
                    section.Options = s.Options;
                    section.OrderIndex = s.OrderIndex;
                    return section;
                });
                group.OrderIndex = g.OrderIndex;
                return group;
            });
            structure.Groups = updatedGroups;
            structure.Rank = formRank;
            structure.FormYear = formYear;
            return Ok(await _formStore.Create(structure));
        }

        [HttpDelete]
        [Route("{formId}")]
        public async Task<IActionResult> RemoveFormById(
            [FromRoute] Guid formId)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }

            var users = await _userStore.Get(identity);
            if (users.All(u => u.UserType != UserType.Admin))
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await _formStore.Delete(formId));
        }
        
        private IFormStructureStore _formStore;    
        private IUserStore _userStore;
    }
}