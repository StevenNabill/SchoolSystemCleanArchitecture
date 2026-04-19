using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.ApplicationUser.Commands.Models;
using SchoolProject.Core.Features.ApplicationUser.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> GetPaginatedList([FromQuery] GetUserPaginationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetById(int id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(id)));
        }
    }
}
