using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using FacultyAPR.Models;
using FacultyAPR.Storage;

namespace FacultyAPR.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        public ReviewController(IFormReviewerStore reviewStore, IUserStore userStore)
        {
            this.reviewStore = reviewStore ?? throw new ArgumentNullException(nameof(reviewStore));
            this.userStore = userStore ?? throw new ArgumentNullException(nameof(userStore));

        }

        [HttpGet]
        [Route("{reviewerId}")]
        public async Task<IActionResult> GetReviewerForms([FromRoute] Guid reviewerId)
        {
            string identity = HttpContext.User.Identity.Name;
            if (identity == default)
            {
                return Unauthorized("No token specified.");
            }

            var users = await userStore.Get(identity);
            if (users.All(u => u.UserType != UserType.Admin
                            && u.UserType != UserType.FacultyChair
                            && u.UserType != UserType.Dean))
            {
                return Unauthorized("User does not have access.");
            }
            return Ok(await this.reviewStore.GetAll(reviewerId));
        }

        private IFormReviewerStore reviewStore;
        private IUserStore userStore;

    }
}