using MediatR;
using SchoolProject.Core.Bases;
using SchoolProject.Core.Features.Students.Queries.Responses;
using SchoolProject.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolProject.Core.Features.Students.Queries.Models
{
    public class GetStudentsListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {
    }
}
