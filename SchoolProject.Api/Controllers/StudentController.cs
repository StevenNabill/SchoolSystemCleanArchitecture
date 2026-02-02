using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Students.Commands.Models;
using SchoolProject.Core.Features.Students.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class StudentController : AppControllerBase
    {
        [HttpGet(Router.StudentRouting.GetStudentsList)]
        public async Task<IActionResult> GetList()
        {
            return NewResult(await Mediator.Send(new GetStudentsListQuery()));
        }
        [HttpGet(Router.StudentRouting.PaginatedList)]
        public async Task<IActionResult> GetPaginatedList([FromQuery] GetStudentPaginatedListQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
        [HttpGet(Router.StudentRouting.GetStudentById)]
        public async Task<IActionResult> GetById(int id)
        {
            return NewResult(await Mediator.Send(new GetStudentByIdQuery(id)));
        }
        [HttpPost(Router.StudentRouting.CreateStudentCommand)]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
        [HttpPut(Router.StudentRouting.EditStudentCommand)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
        [HttpDelete(Router.StudentRouting.DeleteStudentCommand)]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return NewResult(await Mediator.Send(new DeleteStudentCommand(id)));
        }
    }
}
