using Microsoft.AspNetCore.Mvc;
using SchoolProject.Api.Base;
using SchoolProject.Core.Features.Departments.Queries.Models;
using SchoolProject.Data.AppMetaData;

namespace SchoolProject.Api.Controllers
{
    [ApiController]
    public class DepartmentController : AppControllerBase
    {

        #region Actions
        [HttpGet(Router.DepartmentRouting.GetDepartmentById)]
        public async Task<IActionResult> GetById([FromQuery] GetDepartmentByIdQuery query)
        {
            return NewResult(await Mediator.Send(query));
        }
        #endregion

    }
}
