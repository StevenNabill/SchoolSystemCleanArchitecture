using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Authentication.Commands.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.AuthenticationRouting.CreateStudentCommand)]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
