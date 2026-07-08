using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Core.Features.Authentication.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
        [HttpPost]
        [Route(Router.AuthenticationRouting.RefreshToken)]
        public async Task<IActionResult> RefreshToken([FromForm] RefreshTokenCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
        [HttpGet]
        [Route(Router.AuthenticationRouting.ValidateToken)]
        public async Task<IActionResult> ValidateToken([FromQuery] AuthorizeUserQuery query)
        {
            return NewResult(await Mediator.Send(query));
        }
    }
}
