using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using InsuranceInc.Core.Models;
using InsuranceInc.Business.Services;

namespace InsuranceInc.WebApi.Controllers
{
    //
    // The ASP.NET Core users controller defines and handles all routes / endpoints for the api that relate
    // to users, this includes authentication and standard CRUD operations. Within each route the controller
    // calls the user service to perform the action required, this enables the controller to stay 'lean' and
    // completely separated from the business logic and data access code.
    //
    // The controller actions are secured with basic authentication using the [Authorize] attribute, with the
    // exception of the Authenticate method which allows public access by overriding the [Authorize] attribute
    // on the controller with the [AllowAnonymous] attribute on the action method. I chose this approach so
    // any new action methods added to the controller will be secure by default unless explicitly made public.
    //

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        #region Public Methods

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.UserName, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
            else
            {
                return Ok(user);
            }
        }

        #endregion
    }
}
