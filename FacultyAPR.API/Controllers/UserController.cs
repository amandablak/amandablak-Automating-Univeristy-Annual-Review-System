using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using FacultyAPR.Models;
using FacultyAPR.Storage;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
namespace FacultyAPR.API.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        static readonly string[] scopeRequiredByApi = new string[] { "access_as_user" };

        public UserController(IUserStore userStore)
        {
            if (userStore == default) { throw new ArgumentNullException(nameof(userStore)); }

            this.userStore = userStore;
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody] User userData)
        {
            string identity = HttpContext.Request.Headers[AuthorizationHeader];
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await userStore.Get(identity);
            if (users.Any(u => u.UserType != UserType.Admin ))
            {
                return Unauthorized("User does not have access.");
            }
            
            // create new user form supplied data in the body/payload of request
            userData.Id = Guid.NewGuid();
            return Ok(await userStore.Create(userData));
        }

        // need to specifiy what type of user is being deleted
        // ex: interm chair has FacultyUser + APRReviewer. 
        //     After new chair is found, only delete APRReviewer
        [HttpDelete]
        [Route("{facultyId}/{type}")]
        public async Task<IActionResult> RemoveUser(
            [FromRoute] User userPayload)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await userStore.Get(identity);
            if (users.Any(u => u.UserType != UserType.Admin ))
            {
                return Unauthorized("User does not have access.");
            }
            
            if (userPayload == default) { return BadRequest("User payload must not be empty"); }
            return Ok(await userStore.Remove(userPayload));
        }

        // patch/update part of a user entry. Body/payload of request should have the new data
        [HttpPatch]
        [Route("")]
        public async Task<IActionResult> UpdateUserByIdAndType([FromBody] User UserData)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await userStore.Get(identity);
            if (users.All(u => u.UserType != UserType.Admin ))
            {
                return Unauthorized("User does not have access.");
            }
            
            if (UserData == default) { return BadRequest("User payload must not be empty"); }
            // if the payload data is equal to db data, no need to update
            // else update the specific db fields as supplied in the payload
            // ex: if (UserData.name != dbData.name) db.update(id=facultyid, type=type name=UserData.name);
            return Ok(await userStore.Update(UserData));
        }

        // a facultyId may have more than one user type associated with it
        // ex: a chair is both a FacultyUser and APRReviewer
        // so those users may have a list with 2+ entires
        // normal faculty will have a list with 1 entry (the FacultyUser class)
        [HttpGet]
        [Route("{email}")]
        public async Task<IActionResult> GetUsersRegisteredByEmail([FromRoute] string email)
        {
            if (string.IsNullOrEmpty(email)) { return BadRequest("Email must not be empty"); }

            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await userStore.Get(identity);
            if (!users.Any())
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await userStore.Get(email));
        }

        // admin thing to get all the people
        // use auth OR garuntee that only admin front-ends call this endpoint
        // thsi might be in another controller specific to admin b/c of auth
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }
            
            var users = await userStore.Get(identity);
            if (users.All(u => u.UserType != UserType.Admin ))
            {
                return Unauthorized("User does not have access.");
            }
            
            return Ok(await userStore.GetAll());
        }

        // Get the reveiwr for a user based on what department they are in and what rank they are.
        // if user is chair, set reviewr to dean, else check department
        [HttpGet]
        [Route("{facultyId}/{rank}")]
        public async Task<IActionResult> GetReviewerByFaculty([FromRoute] Guid facultyId, [FromRoute] UserType rank) {
            return Ok(await userStore.GetReviewerByFaculty(facultyId, rank));
        }

        private readonly IUserStore userStore;
        private static readonly string AuthorizationHeader = "Authentication";
    }
}
